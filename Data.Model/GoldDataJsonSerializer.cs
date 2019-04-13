using Newtonsoft.Json;
using System;

namespace Data.Model
{
    public class GoldDataJsonSerializer : ISerializer<GoldDataModel>
    {
        public GoldDataModel Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;

            if (json.Contains(Environment.NewLine)) //TODO get rid of those newlines where they were generated
            {
                json = json.Replace(Environment.NewLine, string.Empty);
            }

            var goldData = JsonConvert.DeserializeObject<GoldDataModel>(json);

            return goldData;
        }

        public string Serialize(GoldDataModel t)
        {
            throw new NotImplementedException("currently not required as external api in one way response only communication and frontend application handles reparsing on its own");
        }
    }
}
