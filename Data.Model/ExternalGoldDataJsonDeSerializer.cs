using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Model
{
    public class ExternalGoldDataJsonDeSerializer : ISerializer<ExternalGoldDataModel>
    {
        public ExternalGoldDataModel Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;

            if (json.Contains(Environment.NewLine)) //TODO get rid of those newlines where they were generated
            {
                json = json.Replace(Environment.NewLine, string.Empty);
            }

            var goldData = JsonConvert.DeserializeObject<ExternalGoldDataModel>(json);

            return goldData;
        }

        public string Serialize(ExternalGoldDataModel goldDataModel)
        {
            throw new NotImplementedException("currently not required as external api in one way response only communication and frontend application handles reparsing on its own");

            //return JsonConvert.SerializeObject(goldDataModel);
        }

        public string Serialize(Dictionary<DateTime, double> dailyGoldPrices)
        {
            return JsonConvert.SerializeObject(dailyGoldPrices);
        }

        public static Dictionary<DateTime, double> GetDailyGoldDataFromUnparsedExternalJson(List<List<object>> data)
        {
            var dailyGoldPrices = new Dictionary<DateTime, double>();

            foreach (var dayData in data)
            {
                if (!DateTime.TryParse(dayData.First().ToString(), out DateTime key) ||
                    !double.TryParse(dayData.Last().ToString(), out double value)) continue;

                dailyGoldPrices.Add(key, value);
            }

            return dailyGoldPrices;
        }
    }
}
