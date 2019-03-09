using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class GoldDataModel
    {
        public const string NEWEST_AVAILALE_DATE = "newest_available_date";
        public const string OLDEST_AVAILABLE_DATE = "oldest_available_date";
        public const string DATA = "data";

        [JsonProperty(NEWEST_AVAILALE_DATE)]
        public string NewestAvailaleDate { get; set; }

        [JsonProperty(OLDEST_AVAILABLE_DATE)]
        public string OldestAvailableDate { get; set; }

        [JsonProperty(DATA)]
        //public Dictionary<string, double> Data { get; set; }
        //public List<object> Data { get; set; }
        //public List<DailyExchangeRate> Data { get; set; }
        public List<List<object>> Data { get; set; }
    }

    public class DailyExchangeRate
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var s = Get();
            var r = s.Result;

            var goldDataOverview = AllChildren(JObject.Parse(r))
                .First(c => c.Path.Contains("dataset"))
                .Children<JObject>()
                .First();

            var g = JsonConvert.DeserializeObject<GoldDataModel>(goldDataOverview.ToString());

            //foreach(var rrr in rr)
            //{
            //    var rrrr = rrr.GetValue("id");
            //}

            Console.WriteLine("Hello World!");
        }

        public static async Task<string> Get()
        {
            var client = HttpClientFactory.Create();
            var httpResponse = await client.GetAsync("https://www.quandl.com/api/v3/datasets/WGC/GOLD_DAILY_AUD.json");
            var body = await httpResponse.Content.ReadAsStringAsync();

            return body;
        }

        private static IEnumerable<JToken> AllChildren(JToken json)
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
    }
}
