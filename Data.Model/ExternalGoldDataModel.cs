using Data.Model.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Model
{
    public class ExternalGoldDataModel
    {
        //names comes from external api json structure
        public const string NEWEST_AVAILALE_DATE = "newest_available_date";
        public const string OLDEST_AVAILABLE_DATE = "oldest_available_date";
        public const string DATA = "data";

        [JsonProperty(NEWEST_AVAILALE_DATE)]
        public string NewestAvailaleDate { get; set; }

        [JsonProperty(OLDEST_AVAILABLE_DATE)]
        public string OldestAvailableDate { get; set; }

        [JsonProperty(DATA)]
        public List<List<object>> Data { get; set; }

        public Dictionary<DateTime, double> DailyGoldPrices => 
            GoldDataJsonModifier.GetDailyGoldDataFromUnparsedExternalJson(Data);

        public GoldPrices GoldPrices => new GoldPrices
        {
            Prices = GoldDataJsonModifier.GetDailyGoldDataFromUnparsedExternalJson(Data)
                .Select(d => new GoldPriceDay
                {
                    Date = d.Key,
                    Price = d.Value
                }).ToList()
        };
    }
}
