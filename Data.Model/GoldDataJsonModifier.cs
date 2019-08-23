using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Model
{
    public class GoldDataJsonModifier
    {
        public static Dictionary<DateTime, double> GetDailyGoldDataFromUnparsedExternalJson(List<List<object>> data)
        {
            var dailyGoldPrices = new Dictionary<DateTime, double>();

            foreach (var dayData in data)
            {
                if (!DateTime.TryParse(dayData.First().ToString(), out DateTime key) ||
                    !double.TryParse(dayData.Last().ToString(), out double value)) continue;

                dailyGoldPrices.Add(key, value);
            }

            return dailyGoldPrices;
        }
    }
}
