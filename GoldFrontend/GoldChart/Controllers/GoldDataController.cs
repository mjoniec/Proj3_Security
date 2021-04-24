using System.Net.Http;
using System.Threading.Tasks;
using Data.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace GoldChart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldDataController : ControllerBase
    {
        private const string BackupGoldPricesStaticResourceName = "GoldPricesExampleFrontendBackup.json";

        [HttpGet("[action]")]
        public string GoldDaily()
        {
            GoldPrices goldPrices = null;

            try
            {
                goldPrices = GetGoldPrices(GetGoldPricesFromBackendService());
            }
            catch
            {
                
            }

            if (goldPrices == null)
            {
                goldPrices = GetGoldPrices(GetGoldPricesFromBackup());
            }

            return GoldPricesSerializer.Serialize(goldPrices);
        }

        private static GoldPrices GetGoldPrices(Task<GoldPrices> taskBackup)
        {
            GoldPrices goldPrices;

            taskBackup.Wait();

            goldPrices = taskBackup.Result;

            return goldPrices;
        }

        private async Task<GoldPrices> GetGoldPricesFromBackendService()
        {
            var client = HttpClientFactory.Create();
            var httpResponse = await client.GetAsync("https://localhost:44350/Gold/GetDataPrepared");
            var body = await httpResponse.Content.ReadAsStringAsync();
            var isRequestIdValid = ushort.TryParse(body, out var requestId);

            if (!isRequestIdValid) return null;

            System.Threading.Thread.Sleep(3000);

            var httpResponse2 = await client.GetAsync("https://localhost:44350/Gold/GetData/" + requestId);
            var goldPrices = await httpResponse2.Content.ReadAsAsync<GoldPrices>();

            return goldPrices;
        }

        private async Task<GoldPrices> GetGoldPricesFromBackup()
        {
            var client = HttpClientFactory.Create();
            var httpResponse = await client.GetAsync($"{Request.Scheme}://{Request.Host.Value}/{BackupGoldPricesStaticResourceName}");
            var goldPrices = await httpResponse.Content.ReadAsAsync<GoldPrices>();

            return goldPrices;
        }
    }
}
