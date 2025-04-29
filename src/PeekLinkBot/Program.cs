using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;
using System.Net.Http.Headers;
using Serilog;
using MongoDB.Driver;
using PeekLinkBot.Configuration;

namespace PeekLinkBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return new HostBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddEnvironmentVariables("PEEK_LINK_BOT_");

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddOptions();

                    services.AddHttpClient("Reddit", httpClient =>
                    {
                        httpClient.BaseAddress = new Uri("https://oauth.reddit.com");
                        httpClient
                            .DefaultRequestHeaders
                            .UserAgent
                            .Add(new ProductInfoHeaderValue("PeekLinkBot", "1.0"));
                    });

                    services.AddHttpClient("RedditAuth", httpClient =>
                    {
                        httpClient.BaseAddress = new Uri("https://www.reddit.com");
                        httpClient
                            .DefaultRequestHeaders
                            .UserAgent
                            .Add(new ProductInfoHeaderValue("PeekLinkBot", "1.0"));
                    });

                    services.Configure<PeekLinkBotConfig>(context.Configuration);

                    services.AddSingleton<IHostedService, PeekLinkBotService>();

                    services.ConfigureApplicationDatabase();
                })
                .UseSerilog((hostingContext, services, config) => 
                {
                    config
                        .MinimumLevel.Debug()
                        .WriteTo.Console(
                            outputTemplate:
                                "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                        )
                        .WriteTo.File(
                            "log/.log",
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true,
                            outputTemplate:
                                "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                        );
                });
        }
    }
}
