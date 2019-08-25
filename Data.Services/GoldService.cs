using Data.Model.Common;
using Mqtt.Client;
using System;

namespace Data.Services
{
    public class GoldService : IGoldService
    {
        //this can not be readonly, must be assigned if mqtt connection is lost and back again todo #32
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

            //we may check if requestId has been posted in start preparing data or remove the request id entirely // todo # 

            //if requestId is valid but mqtt is not connected add fallback here todo #32

            return _goldPrices;
        }

        private void ResponseReceivedHandler(object sender, MessageEventArgs e)
        {
            var goldPrices = GoldPricesDeserializer.Deserialize(e.Message);

            if (goldPrices != null) _goldPrices = goldPrices;
        }
    }
}
