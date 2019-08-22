using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    public class GoldDataJsonModifier
    {
        public static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;

                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }

        public static ushort? GetGoldDataIdFromResponseMessage(string message)
        {
            var allChildren = AllChildren(JObject.Parse(message));

            var dataIdString = allChildren
                .First(c => c.Path.Contains("dataId"))
                .Values()
                .First()
                .ToString();

            var parseResult = ushort.TryParse(dataIdString, out var dataId);

            if (!parseResult || dataId == ushort.MinValue) return null;

            return dataId;
        }

        public static string GetGoldDataFromResponseMessage(string message)
        {
            var allChildren = AllChildren(JObject.Parse(message));

            var goldData = allChildren
                .First(c => c.Path.Contains("dataset"))
                .Children<JObject>()
                .First()
                .ToString();

            return goldData;
        }

        public static string AddRequestIdToJson(string dataId, string responseMessage)
        {
            var stringBuilder = new StringBuilder(responseMessage);

            stringBuilder.Remove(0, 1);
            stringBuilder.Insert(0, "{\"dataId\":\"" + dataId + "\",");

            return stringBuilder.ToString();
        }

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
