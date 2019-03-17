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
        private static Dictionary<ushort, string> GoldData;

        private readonly bool _mqttConnected;
        private readonly IGoldRepository _goldRepository;
        private readonly MqttDualTopicClient _mqttDualTopicClient;

        public GoldService(IGoldRepository goldRepository)
        {
            GoldData = new Dictionary<ushort, string>();

            _goldRepository = goldRepository;

            //use DI and app config
            //TODO resolwe scope lifetime - 1 request gets executed before constructor is created
            _mqttDualTopicClient = new MqttDualTopicClient(new MqttDualTopicData(
                "localhost", 1883, "ResponseMqttTopic", "RequestMqttTopic"));

            _mqttDualTopicClient.RaiseMessageReceivedEvent += ResponseReceivedHandler;

            var t = _mqttDualTopicClient.Start();

            _mqttConnected = t.Result;
        }

        private void ResponseReceivedHandler(object sender, MessageEventArgs e)
        {
            //TODO use dictionary here, extract dataid 
            ResponseMessage = e.Message;
        }

        public ushort GetDataPrepared()
        {
            if (!_mqttConnected) return ushort.MinValue;

            var dataId = (ushort) new Random().Next(ushort.MinValue + 1, ushort.MaxValue);

            try
            {
                _mqttDualTopicClient.Send(dataId.ToString());
                GoldData.Add(dataId, string.Empty);
            }
            catch
            {
                return ushort.MinValue;
            }

            return dataId;
        }

        public string GetNewestPrice(string dataId)
        {
            //TODO write a nice unit test for all ushort input case scenario and get coverage percantage
            if (string.IsNullOrEmpty(dataId)|| !_mqttConnected) return string.Empty;

            var parseResult = ushort.TryParse(dataId, out var dataIdParsed);

            if (!parseResult || dataIdParsed == ushort.MinValue) return string.Empty;
            
            //TODO return to dict when implemented
            //var isDataPresent = GoldData.TryGetValue(dataIdParsed, out var responseMessage);

            //if (!isDataPresent) return string.Empty;

            if (!string.IsNullOrEmpty(ResponseMessage))
            {
                var goldDataOverview =
                    AllChildren(JObject.Parse(ResponseMessage))
                    .First(c => c.Path.Contains("dataset"))
                    .Children<JObject>()
                    .First();

                var goldDataDeserialized = JsonConvert.DeserializeObject<GoldDataModel>(goldDataOverview.ToString());

                return goldDataDeserialized.NewestAvailaleDate;
            }

            return "empty qqq";

            //var goldData = _goldRepository.Get();
            //DateTime.TryParse(goldData.NewestAvailaleDate, out DateTime date);

            //goldData.DailyGoldData.TryGetValue(date, out double value);

            //return value.ToString();
        }

        //IDictionary<DateTime, double> IGoldService.GetAllGoldPriceData(ushort dataId)
        //{
        //    var goldData = _goldRepository.Get();

        //    return goldData.DailyGoldData;
        //}


        //TODO refactor this to some JSON parser class
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
