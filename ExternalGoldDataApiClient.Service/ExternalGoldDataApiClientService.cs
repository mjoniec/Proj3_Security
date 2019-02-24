using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace ExternalGoldDataApiClient.Service
{
    public class ExternalGoldDataApiClientService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IOptions<ExternalGoldDataApiClientConfig> _config;

        public ExternalGoldDataApiClientService(ILogger<ExternalGoldDataApiClientConfig> logger, IOptions<ExternalGoldDataApiClientConfig> config)
        {
            _logger = logger;
            _config = config;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting external gold data api client service: " + _config.Value.Name);

            GetGoldData();

            return Task.CompletedTask;
        }

        private void GetGoldData()
        {
            var goldPricesClient = new GoldPricesClient();
            string s = string.Empty;

            goldPricesClient.Get().ContinueWith(t =>
            {
                s = t.Result;
            })
            .Wait();

            _logger.LogInformation(s);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping external gold data api client service.");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("Disposing....");
        }
    }

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

    public class ExternalGoldDataApiClientConfig
    {
        public string Name { get; set; }
    }
}
