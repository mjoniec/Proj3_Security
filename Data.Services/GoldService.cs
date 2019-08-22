using Data.Model;
using Data.Model.Common;
using Mqtt.Client;
using System;
using System.Collections.Generic;

namespace Data.Services
{
    public class GoldService : IGoldService
    {
        private readonly bool _mqttConnected;
        private readonly IMqttDualTopicClient _mqttDualTopicClient;
        private readonly Dictionary<ushort, GoldPrices> _goldPricesResponsesByRequestId;

        public bool IsMqttConnected => _mqttConnected;

        public GoldService(IMqttDualTopicClient mqttDualTopicClient)
        {
            _goldPricesResponsesByRequestId = new Dictionary<ushort, GoldPrices>();
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

            //internal logic assuming valid data is not to be ushort min - redo it somehow??
            var requestId = (ushort)new Random().Next(ushort.MinValue + 1, ushort.MaxValue);

            try
            {
                _mqttDualTopicClient.Send(requestId.ToString());
                _goldPricesResponsesByRequestId.Add(requestId, null);
            }
            catch
            {
                return ushort.MinValue;
            }

            return requestId;
        }

        public GoldPrices GetDailyGoldPrices(ushort requestId)
        {
            //internal logic assuming valid data is not to be ushort min - redo it somehow??
            if (requestId == ushort.MinValue) return null;

            var isDataPresent = _goldPricesResponsesByRequestId.TryGetValue(requestId, out var goldPrices);

            if (!isDataPresent) return null;

            return goldPrices;
        }

        //TODO issue #19 create logger and custom Exception for all erroneous cases in ResponseReceivedHandler and GetNewestPrice
        private void ResponseReceivedHandler(object sender, MessageEventArgs e)
        {
            //TODO - get rid of these here
            var requestId = GoldDataJsonModifier.GetGoldDataIdFromResponseMessage(e.Message);
            var goldPrices = GoldDataJsonModifier.GetGoldDataFromResponseMessage(e.Message);

            if (requestId == null || !_goldPricesResponsesByRequestId.ContainsKey(requestId.Value)) return;

            _goldPricesResponsesByRequestId[requestId.Value] = goldPrices;
        }
    }
}

