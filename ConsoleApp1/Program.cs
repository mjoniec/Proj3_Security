using Data.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
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

            var goldData = JsonConvert.DeserializeObject<GoldDataModel>(goldDataOverview.ToString());
            var today = DateTime.Now.AddDays(-60);

            if (goldData.DailyGoldData.TryGetValue(today.Date, out double tst))
            {
                Console.WriteLine("Hello World!" + tst);
            }
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
