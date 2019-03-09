using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Model
{
    public class GoldDataModel
    {
        private Dictionary<DateTime, double> _dailyGoldData;

        public Dictionary<DateTime, double> DailyGoldData
        {
            get
            {
                if (_dailyGoldData != null) return _dailyGoldData;

                _dailyGoldData = new Dictionary<DateTime, double>();

                foreach (var dayData in DailyGoldDataUnparsed)
                {
                    if (!DateTime.TryParse(dayData.First().ToString(), out DateTime key) ||
                        !double.TryParse(dayData.Last().ToString(), out double value)) continue;

                    _dailyGoldData.Add(key, value);
                }

                return _dailyGoldData;
            }
        }

        public const string NEWEST_AVAILALE_DATE = "newest_available_date";
        public const string OLDEST_AVAILABLE_DATE = "oldest_available_date";
        public const string DATA = "data";

        [JsonProperty(NEWEST_AVAILALE_DATE)]
        public string NewestAvailaleDate { get; set; }

        [JsonProperty(OLDEST_AVAILABLE_DATE)]
        public string OldestAvailableDate { get; set; }

        [JsonProperty(DATA)]
        public List<List<object>> DailyGoldDataUnparsed { get; set; }
    }
}
