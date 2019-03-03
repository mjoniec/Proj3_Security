using System.Net.Http;
using System.Threading.Tasks;

namespace ExternalGoldDataApiClient.Service
{
    public class GoldPricesClient
    {
        public async Task<string> Get()
        {
            var client = HttpClientFactory.Create();
            var httpResponse = await client.GetAsync("https://www.quandl.com/api/v3/datasets/WGC/GOLD_DAILY_AUD.json");
            var body = await httpResponse.Content.ReadAsStringAsync();

            return body;
        }
    }
}
