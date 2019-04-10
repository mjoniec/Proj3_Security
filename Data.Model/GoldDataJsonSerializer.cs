using System;

namespace Data.Model
{
    public class GoldDataJsonSerializer : JsonSerializer, ISerializer<GoldDataModel>
    {
        public GoldDataModel Deserialize(string json)
        {
            throw new NotImplementedException();
        }

        public string Serialize(GoldDataModel t)
        {
            throw new NotImplementedException("required by frontend application");
        }
    }
}
