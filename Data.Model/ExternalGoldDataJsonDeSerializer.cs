using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Model
{
    public class ExternalGoldDataJsonDeserializer
    {
        public ExternalGoldDataModel DeserializeDataFromMessage(string message)
        {
            //if (json.Contains(Environment.NewLine)) //TODO get rid of those newlines where they were generated
            //{
            //    json = json.Replace(Environment.NewLine, string.Empty);
            //}

            string goldDataJson = ExtractDailyGoldPricesFromExternalJson(message);

            var goldData = JsonConvert.DeserializeObject<ExternalGoldDataModel>(goldDataJson);

            return goldData;
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

        private string ExtractDailyGoldPricesFromExternalJson(string message)
        {
            var allChildren = AllChildren(JObject.Parse(message));

            var goldData = allChildren
                .First(c => c.Path.Contains("dataset"))
                .Children<JObject>()
                .First()
                .ToString();

            return goldData;
        }

        public string Serialize(ExternalGoldDataModel goldDataModel)
        {
            throw new NotImplementedException("currently not required as external api in one way response only communication and frontend application handles reparsing on its own");
        }
    }
}
