using Data.Model;
using Data.Repositories;
using Mqtt.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Services
{
    public class GoldService : IGoldService
    {
        private static string ResponseMessage = "";
        IGoldRepository _goldRepository;
        MqttDualTopicClient _mqttDualTopicClient;

        public GoldService(IGoldRepository goldRepository)
        {
            _goldRepository = goldRepository;

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

                if (!string.IsNullOrEmpty(ResponseMessage))
                {
                    var goldDataOverview = AllChildren(JObject.Parse(ResponseMessage))
                        .First(c => c.Path.Contains("dataset"))
                        .Children<JObject>()
                        .First();

                    var goldDataDeserialized = JsonConvert.DeserializeObject<GoldDataModel>(goldDataOverview.ToString());

                    return goldDataDeserialized.NewestAvailaleDate;
                }

                var goldData = _goldRepository.Get();
                DateTime.TryParse(goldData.NewestAvailaleDate, out DateTime date);

                goldData.DailyGoldData.TryGetValue(date, out double value);

                return value.ToString();
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;

                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }
    }
}
