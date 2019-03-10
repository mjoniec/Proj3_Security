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

            //use DI and app config
            //TODO resolwe scope lifetime - 1 request gets executed before constructor is created
            _mqttDualTopicClient = new MqttDualTopicClient(
                "localhost", 1883, "ResponseMqttTopic", "RequestMqttTopic", ResponseReceivedHandler);
        }

        //Move this to service and use DI
        public string ResponseReceivedHandler(string message)
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
            try
            {
                _mqttDualTopicClient.Send("request");

                if (!string.IsNullOrEmpty(ResponseMessage)) return ResponseMessage;

                var goldData = _goldRepository.Get();
                DateTime.TryParse(goldData.NewestAvailaleDate, out DateTime date);

                goldData.DailyGoldData.TryGetValue(date, out double value);

                return value.ToString();
            }
            catch
            {
                return "qq";
            }
        }

        public DateTime GetOldestDay()
        {
            var goldData = _goldRepository.Get();

            DateTime.TryParse(goldData.OldestAvailableDate, out DateTime date);

            return date;
        }
    }
}
