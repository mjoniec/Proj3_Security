using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    public class GoldDataModel
    {
        //names comes from external api json structure
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
        public List<List<object>> Data
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

                foreach (var dayData in Data)
                {
                    if (!DateTime.TryParse(dayData.First().ToString(), out DateTime key) ||
                        !double.TryParse(dayData.Last().ToString(), out double value)) continue;

                    _dailyGoldPrices.Add(key, value);
                }

                return _dailyGoldPrices;
            }
        }

        public static string AddRequestIdToJson(string dataId, string responseMessage)
        {
            var stringBuilder = new StringBuilder(responseMessage);

            stringBuilder.Remove(0, 1);
            stringBuilder.Insert(0, "{\"dataId\":\"" + dataId + "\",");

            return stringBuilder.ToString();
        }
    }
}
