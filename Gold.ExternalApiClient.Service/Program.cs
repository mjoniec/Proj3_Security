using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mqtt.Client;

namespace Gold.ExternalApiClient.Service
{
    class Program
    {
        //invoke with cmd
        //dotnet run --ExternalGoldDataApiClient:Name="Gold data service"

        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<GoldExternalApiClientConfig>(hostContext.Configuration.GetSection("GoldExternalApiClient"));
                    services.AddScoped<IMqttDualTopicClient>(x => 
                    {
                        //TODO - use config
                        return new MqttDualTopicClient(new MqttDualTopicData(
                            "localhost", 1883, "RequestMqttTopic", "ResponseMqttTopic"));
                    });
                    services.AddSingleton<IHostedService, GoldExternalApiClientService>();
                })
                .ConfigureLogging((hostingContext, logging) => {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }
    }
}
