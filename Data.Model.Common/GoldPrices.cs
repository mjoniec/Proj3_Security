using Newtonsoft.Json;
using System.Collections.Generic;

namespace Data.Model.Common
{
    public class GoldPrices
    {
        public List<GoldPriceDay> Prices { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static GoldPrices
    }
}
