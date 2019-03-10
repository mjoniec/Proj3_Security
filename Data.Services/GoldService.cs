using Data.Repositories;
using Mqtt.Client;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Data.Services
{
    public class GoldService : IGoldService
    {
        private static string ResponseMessage = "";
        IGoldRepository _goldRepository;
        IMqttDualTopicClient _mqttDualTopicClient;

        public GoldService(IGoldRepository goldRepository/*, IMqttDualTopicClient mqttDualTopicClient*/)
        {
            _goldRepository = goldRepository;
            //_mqttDualTopicClient = mqttDualTopicClient;

            ////use DI and app config
            _mqttDualTopicClient = new MqttDualTopicClient(
                "localhost", 1883, "ResponseMqttTopic", "RequestMqttTopic", ResponseReceivedHandler);
        }

        //Move this to service and use DI
        private string ResponseReceivedHandler(string message)
        {
            ResponseMessage = message;

            return message;
        }

        IDictionary<DateTime, double> IGoldService.GetAllPriceData()
        {
            var goldData = _goldRepository.Get();

            return goldData.DailyGoldData;
        }

        public string GetNewestPrice()
        {
            _mqttDualTopicClient.Send("request");

            Thread.Sleep(1000);

            if (!string.IsNullOrEmpty(ResponseMessage)) return ResponseMessage;

            var goldData = _goldRepository.Get();
            DateTime.TryParse(goldData.NewestAvailaleDate, out DateTime date);

            goldData.DailyGoldData.TryGetValue(date, out double value);

            return value.ToString();
        }

        public DateTime GetOldestDay()
        {
            var goldData = _goldRepository.Get();

            DateTime.TryParse(goldData.OldestAvailableDate, out DateTime date);

            return date;
        }
    }
}
