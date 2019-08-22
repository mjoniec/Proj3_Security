using Newtonsoft.Json;
using System;

namespace Data.Model
{
    public class ExternalGoldDataJsonDeSerializer : IDeSerializer<ExternalGoldDataModel>
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
        }
    }
}
