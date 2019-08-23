using Data.Model.Common;
using Newtonsoft.Json;

namespace Data.Model
{
    public class GoldPricesDeSerializer : IDeSerializer<GoldPrices>
    {
        public GoldPrices Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;

            var goldData = JsonConvert.DeserializeObject<GoldPrices>(json);

            return goldData;
        }

        public string Serialize(GoldPrices goldPrices)
        {
            return JsonConvert.SerializeObject(goldPrices);
        }
    }
}
