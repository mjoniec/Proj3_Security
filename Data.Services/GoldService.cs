using Data.Model;
using Data.Repositories;
using Mqtt.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Data.Services
{
    public class GoldService : IGoldService
    {
        private readonly bool _mqttConnected;
        private readonly IGoldRepository _goldRepository;
        private readonly MqttDualTopicClient _mqttDualTopicClient;
        private readonly Dictionary<ushort, string> _goldData;

        public GoldService(IGoldRepository goldRepository)
        {
            _goldRepository = goldRepository;
            _goldData = new Dictionary<ushort, string>();

            //TODO use DI and app config
            _mqttDualTopicClient = new MqttDualTopicClient(new MqttDualTopicData(
                "localhost", 1883, "ResponseMqttTopic", "RequestMqttTopic"));

            _mqttDualTopicClient.RaiseMessageReceivedEvent += ResponseReceivedHandler;

            var t = _mqttDualTopicClient.Start();

            _mqttConnected = t.Result;
        }

        //#15 task
        //fuze constructors, use common code that works for application and test
        public GoldService(IGoldRepository goldRepository, /*TODO Use interface*/ MqttDualTopicClient mqttDualTopicClient)
        {
            _goldRepository = goldRepository;
            _goldData = new Dictionary<ushort, string>();
        }

        //TODO issue #19 create logger and custom Exception for all erroneous cases in ResponseReceivedHandler and GetNewestPrice
        private void ResponseReceivedHandler(object sender, MessageEventArgs e)
        {
            var dataId = AllChildren(JObject.Parse(e.Message))
                .First(c => c.Path.Contains("dataId"))
                .Values()
                .First()
                .ToString();

            var parseResult = ushort.TryParse(dataId, out var dataIdParsed);

            if (!parseResult || dataIdParsed == ushort.MinValue) return;

            var goldDataOverview =
                AllChildren(JObject.Parse(e.Message))
                .First(c => c.Path.Contains("dataset"))
                .Children<JObject>()
                .First()
                .ToString();

            if (!_goldData.TryGetValue(dataIdParsed, out var value)) return;

            _goldData[dataIdParsed] = goldDataOverview;
        }

        public ushort StartPreparingData()
        {
            if (!_mqttConnected) return ushort.MinValue;

            var dataId = (ushort) new Random().Next(ushort.MinValue + 1, ushort.MaxValue);

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
        public string GetNewestPrice(string dataId)
        {
            //TODO write unit tests for all ushort input case scenarios and get coverage percantage
            if (string.IsNullOrEmpty(dataId)|| !_mqttConnected) return string.Empty;

            var parseResult = ushort.TryParse(dataId, out var dataIdParsed);

            if (!parseResult || dataIdParsed == ushort.MinValue) return string.Empty;

            var isDataPresent = _goldData.TryGetValue(dataIdParsed, out var responseMessage);

            if (!isDataPresent) return string.Empty;

            //TODO get rid of those newlines where they were generated
            var responseMessage2 = responseMessage.Replace(Environment.NewLine, string.Empty);

            if (string.IsNullOrEmpty(responseMessage2)) return string.Empty;

            var goldDataDeserialized = JsonConvert.DeserializeObject<GoldDataModel>(responseMessage2);

            return goldDataDeserialized.NewestAvailaleDate;
        }

        public IEnumerable<string> GetAllPrices()
        {
            var goldData = _goldRepository.Get();
            var allPrices = new List<string>();

            //Refactor this functionality into model #10
            foreach(var goldPriceDataValue in goldData.DailyGoldData)
            {
                allPrices.Add(goldPriceDataValue.Key.ToString("yyyy-M-d")
                    + "," 
                    + goldPriceDataValue.Value.ToString(new CultureInfo("en-US")));
            }

            return allPrices;
        }

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
