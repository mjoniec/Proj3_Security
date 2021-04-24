using System.Threading.Tasks;
using Gold.ExternalApiClient.Service.Config.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mqtt.Client;

namespace Gold.ExternalApiClient.Service
{
    public class Program
    {
        //invoke with cmd
        //dotnet run --ExternalGoldDataApiClient:Name="Gold data service"

        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //for some reason on debug it acts as in production, see proj/env vars #22
                    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);
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
                    services.Configure<MqttConfig>(hostContext.Configuration.GetSection("Mqtt"));
                    services.AddScoped<IMqttDualTopicClient>(x => 
                    {
                        return new MqttDualTopicClient(new MqttDualTopicData(
                            hostContext.Configuration["Mqtt:Ip"],
                            int.Parse(hostContext.Configuration["Mqtt:Port"]),
                            hostContext.Configuration["Mqtt:TopicReceiver"],
                            hostContext.Configuration["Mqtt:TopicSender"]));
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
