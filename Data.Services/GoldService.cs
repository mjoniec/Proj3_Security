using Data.Model;
using Data.Repositories;
using Mqtt.Client;
using System;
using System.Collections.Generic;

namespace Data.Services
{
    public class GoldService : IGoldService
    {
        private readonly bool _mqttConnected;
        private readonly IGoldRepository _goldRepository;
        private readonly IMqttDualTopicClient _mqttDualTopicClient;
        private readonly Dictionary<ushort, string> _goldData;//stores gold data with request key
        private ExternalGoldDataJsonDeSerializer _goldDataJsonSerializer = new ExternalGoldDataJsonDeSerializer();

        public bool IsMqttConnected => _mqttConnected;

        public GoldService(IGoldRepository goldRepository, IMqttDualTopicClient mqttDualTopicClient)
        {
            _goldRepository = goldRepository;
            _goldData = new Dictionary<ushort, string>();
            _mqttDualTopicClient = mqttDualTopicClient;

            _mqttDualTopicClient.RaiseMessageReceivedEvent += ResponseReceivedHandler;

            try
            {
                var t = _mqttDualTopicClient.Start();

                _mqttConnected = t.Result;
            }
            catch
            {
                _mqttConnected = false;
            }
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
        public Dictionary<DateTime, double> GetDailyGoldPrices(string dataIdString)
        {
            //TODO improve unit tests for all ushort input case scenarios and get coverage percantage
            if (string.IsNullOrEmpty(dataIdString) || 
                string.Equals(dataIdString, "0") ||
                !_mqttConnected) return GetDailyGoldPricesFromDatabase();

            var isDataIdValid = ushort.TryParse(dataIdString, out var dataId);

            if (!isDataIdValid || dataId == ushort.MinValue) return null;

            var isDataPresent = _goldData.TryGetValue(dataId, out var responseMessage);

            if (!isDataPresent) return null;

            var goldData = _goldDataJsonSerializer.Deserialize(responseMessage);

            return goldData.DailyGoldPrices;
        }

        public string GetDailyGoldPricesSerialized(string dataIdString)
        {
            return _goldDataJsonSerializer.Serialize(GetDailyGoldPrices(dataIdString));
        }

        private Dictionary<DateTime, double> GetDailyGoldPricesFromDatabase()
        {
            var goldData = _goldRepository.Get();

            return goldData.DailyGoldPrices;
        }

        //TODO issue #19 create logger and custom Exception for all erroneous cases in ResponseReceivedHandler and GetNewestPrice
        private void ResponseReceivedHandler(object sender, MessageEventArgs e)
        {
            var dataId = GoldDataJsonModifier.GetGoldDataIdFromResponseMessage(e.Message);
            var goldData = GoldDataJsonModifier.GetGoldDataFromResponseMessage(e.Message);

            if (dataId == null || !_goldData.TryGetValue(dataId.Value, out var value)) return;

            _goldData[dataId.Value] = goldData;
        }
    }
}

