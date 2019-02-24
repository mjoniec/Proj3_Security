using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

            return Task.CompletedTask;
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
