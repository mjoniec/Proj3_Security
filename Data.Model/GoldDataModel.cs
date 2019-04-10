using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Model
{
    public class GoldDataModel
    {
        public const string NEWEST_AVAILALE_DATE = "newest_available_date";
        public const string OLDEST_AVAILABLE_DATE = "oldest_available_date";
        public const string DATA = "data";

        private List<List<object>> _dailyGoldPricesUnparsed;
        private Dictionary<DateTime, double> _dailyGoldPrices;

        [JsonProperty(NEWEST_AVAILALE_DATE)]
        public string NewestAvailaleDate { get; set; }

        [JsonProperty(OLDEST_AVAILABLE_DATE)]
        public string OldestAvailableDate { get; set; }

        [JsonProperty(DATA)]
        public List<List<object>> DailyGoldPricesUnparsed
        {
            get
            {
                return _dailyGoldPricesUnparsed;
            }
            set
            {
                _dailyGoldPricesUnparsed = value;
                _dailyGoldPrices = null;
            }
        }

        public Dictionary<DateTime, double> DailyGoldPrices
        {
            get
            {
                if (_dailyGoldPrices != null) return _dailyGoldPrices;

                _dailyGoldPrices = new Dictionary<DateTime, double>();

                foreach (var dayData in DailyGoldPricesUnparsed)
                {
                    if (!DateTime.TryParse(dayData.First().ToString(), out DateTime key) ||
                        !double.TryParse(dayData.Last().ToString(), out double value)) continue;

                    _dailyGoldPrices.Add(key, value);
                }

                return _dailyGoldPrices;
            }
        }
    }
}
