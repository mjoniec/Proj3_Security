using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Model.Common;
using Data.Model.External;
using Gold.ExternalApiClient.Service.Config.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mqtt.Client;

namespace Gold.ExternalApiClient.Service
{
    public class GoldExternalApiClientService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IOptions<GoldExternalApiClientConfig> _config;
        private readonly IMqttDualTopicClient _mqttDualTopicClient;
        private ExternalGoldDataJsonDeserializer _externalGoldDataJsonDeSerializer = new ExternalGoldDataJsonDeserializer();

        public GoldExternalApiClientService(ILogger<GoldExternalApiClientConfig> logger, IOptions<GoldExternalApiClientConfig> config, IMqttDualTopicClient mqttDualTopicClient)
        {
            _logger = logger;
            _config = config;
            _mqttDualTopicClient = mqttDualTopicClient;

            _mqttDualTopicClient.RaiseMessageReceivedEvent += RequestReceivedHandler;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            _mqttDualTopicClient.Start();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        //TODO how do we know that in e .essage means data id?????
        public void RequestReceivedHandler(object sender, MessageEventArgs e)
        {
            var goldDataResponseMessage = GetGoldData();
            var externalGoldDataModel = _externalGoldDataJsonDeSerializer.DeserializeDataFromMessage(goldDataResponseMessage);
            var goldPrices = GoldPricesSerializer.Serialize((GoldPrices)externalGoldDataModel);

            _logger.LogInformation(goldPrices);
            _logger.LogInformation(e.Message);

            _mqttDualTopicClient.Send(goldPrices);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting external gold data api client service: " + _config.Value.Name);

            return Task.CompletedTask;
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
}
