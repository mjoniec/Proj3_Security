using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mqtt.CommonLib;

namespace Gold.ExternalApiClient.Service
{
    public class GoldExternalApiClientService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IOptions<ExternalGoldDataApiClientConfig> _config;
        private readonly MqttDoubleChannelClientAsync _mqttDoubleChannelClientAsync;

        public GoldExternalApiClientService(ILogger<ExternalGoldDataApiClientConfig> logger, IOptions<ExternalGoldDataApiClientConfig> config)
        {
            _logger = logger;
            _config = config;

            //field initializer can not reference non static - replace with interface and DI
            _mqttDoubleChannelClientAsync = new MqttDoubleChannelClientAsync(
                "localhost", 1883, "RequestMqttTopic", "ResponseMqttTopic", RequestReceivedHandler);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting external gold data api client service: " + _config.Value.Name);

            return Task.CompletedTask;
        }

        public string RequestReceivedHandler(string message)
        {
            _logger.LogInformation(message);

            var goldData = GetGoldData();

            _mqttDoubleChannelClientAsync.Send(goldData);

            return goldData;
        }

        private string GetGoldData()
        {
            var goldPricesClient = new GoldPricesClient();
            var goldData = string.Empty;

            goldPricesClient.Get().ContinueWith(t =>
            {
                goldData = t.Result;
            })
            .Wait();

            _logger.LogInformation(goldData);

            return goldData;
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

    public class ExternalGoldDataApiClientConfig
    {
        public string Name { get; set; }
    }
}
