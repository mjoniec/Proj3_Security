using Newtonsoft.Json;

namespace Data.Model.Common
{
    public static class GoldPricesSerializer
    {
        public static string Serialize(GoldPrices goldPrices)
        {
            return JsonConvert.SerializeObject(goldPrices);
        }
    }
}
