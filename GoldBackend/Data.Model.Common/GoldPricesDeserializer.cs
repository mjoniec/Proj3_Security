using Newtonsoft.Json;

namespace Data.Model.Common
{
    public static class GoldPricesDeserializer
    {
        public static GoldPrices Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;

            var goldData = JsonConvert.DeserializeObject<GoldPrices>(json);

            return goldData;
        }
    }
}
