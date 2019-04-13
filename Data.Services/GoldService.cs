using Data.Model;
using Data.Repositories;
using Mqtt.Client;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Data.Services
{
    public class GoldService : IGoldService
    {
        private readonly bool _mqttConnected;
        private readonly IGoldRepository _goldRepository;
        private readonly IMqttDualTopicClient _mqttDualTopicClient;
        private readonly Dictionary<ushort, string> _goldData;//stores gold data with request key
        private GoldDataJsonSerializer _goldDataJsonSerializer = new GoldDataJsonSerializer();

        public GoldService(IGoldRepository goldRepository, IMqttDualTopicClient mqttDualTopicClient)
        {
            _goldRepository = goldRepository;
            _goldData = new Dictionary<ushort, string>();
            _mqttDualTopicClient = mqttDualTopicClient;

            _mqttDualTopicClient.RaiseMessageReceivedEvent += ResponseReceivedHandler;

            var t = _mqttDualTopicClient.Start();

            _mqttConnected = t.Result;
        }

        //TODO issue #19 create logger and custom Exception for all erroneous cases in ResponseReceivedHandler and GetNewestPrice
        private void ResponseReceivedHandler(object sender, MessageEventArgs e)
        {
            var dataId = GoldDataJsonModifier.GetGoldDataIdFromResponseMessage(e.Message);
            var goldData = GoldDataJsonModifier.GetGoldDataFromResponseMessage(e.Message);

            if (dataId == null || !_goldData.TryGetValue(dataId.Value, out var value)) return;

            _goldData[dataId.Value] = goldData;
        }

        public ushort StartPreparingData()
        {
            if (!_mqttConnected) return ushort.MinValue;

            var dataId = (ushort)new Random().Next(ushort.MinValue + 1, ushort.MaxValue);

            try
            {
                _mqttDualTopicClient.Send(dataId.ToString());
                _goldData.Add(dataId, string.Empty);
            }
            catch
            {
                return ushort.MinValue;
            }

            return dataId;
        }

        //TODO issue #19 create logger and custom Exception for all erroneous cases in ResponseReceivedHandler and GetNewestPrice
        public IEnumerable<string> GetAll(string dataIdString)
        {
            //TODO write unit tests for all ushort input case scenarios and get coverage percantage
            if (string.IsNullOrEmpty(dataIdString) || !_mqttConnected) return null;

            var isDataIdValid = ushort.TryParse(dataIdString, out var dataId);

            if (!isDataIdValid || dataId == ushort.MinValue) return null;

            var isDataPresent = _goldData.TryGetValue(dataId, out var responseMessage);

            if (!isDataPresent) return null;

            var goldData = _goldDataJsonSerializer.Deserialize(responseMessage);
            var allPrices = new List<string>();

            //Refactor this functionality into model #10
            foreach (var goldPriceDataValue in goldData.DailyGoldPrices)
            {
                allPrices.Add(goldPriceDataValue.Key.ToString("yyyy-M-d")
                    + ","
                    + goldPriceDataValue.Value.ToString(new CultureInfo("en-US")));
            }

            return allPrices;
        }

        public IEnumerable<string> GetAllPrices()
        {
            var goldData = _goldRepository.Get();
            var allPrices = new List<string>();

            //Refactor this functionality into model #10
            foreach (var goldPriceDataValue in goldData.DailyGoldPrices)
            {
                allPrices.Add(goldPriceDataValue.Key.ToString("yyyy-M-d")
                    + ","
                    + goldPriceDataValue.Value.ToString(new CultureInfo("en-US")));
            }

            return allPrices;
        }
    }
}

