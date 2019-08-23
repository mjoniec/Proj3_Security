using Data.Model;
using Data.Model.Common;
using Mqtt.Client;
using System;

namespace Data.Services
{
    public class GoldService : IGoldService
    {
        private readonly bool _mqttConnected;
        private readonly IMqttDualTopicClient _mqttDualTopicClient;
        private GoldPrices _goldPrices;

        public bool IsMqttConnected => _mqttConnected;

        public GoldService(IMqttDualTopicClient mqttDualTopicClient)
        {
            _mqttDualTopicClient = mqttDualTopicClient;

            _mqttDualTopicClient.RaiseMessageReceivedEvent += ResponseReceivedHandler;

            try
            {
                var task = _mqttDualTopicClient.Start();

                _mqttConnected = task.Result;
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
            }
            catch
            {
                return ushort.MinValue;
            }

            return requestId;
        }

        public GoldPrices GetDailyGoldPrices(ushort requestId)
        {
            //internal logic assuming valid requestId is not to be ushort min - redo it somehow??
            if (requestId == ushort.MinValue) return null;

            return _goldPrices;
        }

        //TODO issue #19 create logger and custom Exception for all erroneous cases in ResponseReceivedHandler and GetNewestPrice
        private void ResponseReceivedHandler(object sender, MessageEventArgs e)
        {
            //TODO - get rid of these here
            var goldPrices = GoldDataJsonModifier.GetGoldDataFromResponseMessage(e.Message);

            if (goldPrices != null) _goldPrices = goldPrices;
        }
    }
}

