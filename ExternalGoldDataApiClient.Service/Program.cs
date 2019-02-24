using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExternalGoldDataApiClient.Service
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

                    //section name the same as in invoke command
                    //TODO - move to config
                    services.Configure<ExternalGoldDataApiClientConfig>(hostContext.Configuration.GetSection("ExternalGoldDataApiClient"));
                    services.AddSingleton<IHostedService, ExternalGoldDataApiClientService>();
                })
                .ConfigureLogging((hostingContext, logging) => {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }
    }
}
