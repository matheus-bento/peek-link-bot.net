using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeekLinkBot.Api.Configuration;
using Serilog.Extensions;
using Serilog.Extensions.Options;

namespace PeekLinkBot.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplication app =  CreateHostBuilder(args).Build();

            app.MapControllers();

            app.Run();
        }

        private static WebApplicationBuilder CreateHostBuilder(string[] args)
        {
            var webAppBuilder =  WebApplication.CreateBuilder(args);

            ConfigureConfigurationProviders(webAppBuilder);
            ConfigureServices(webAppBuilder);

            webAppBuilder.UseSeriLogger(new SerilogOptions
            {
                MinimumLevel = new MinimumLevelOptions
                {
                    Default = "Information"
                },
                WriteTo =
                [
                    new WriteToOptions
                    {
                        Name = "Console",
                        Args = new ArgsOptions
                        {
                            OutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                        }
                    },
                    new WriteToOptions
                    {
                        Name = "File",
                        Args = new ArgsOptions
                        {
                            Path = "log/.log",
                            RollingInterval = "Day",
                            RollOnFileSizeLimit = true,
                            OutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                        }
                    }
                ]
            });

            return webAppBuilder;
        }

        private static void ConfigureConfigurationProviders(IHostApplicationBuilder builder)
        {
            builder.Configuration.AddEnvironmentVariables("PEEK_LINK_BOT_API_");
        }

        private static void ConfigureServices(IHostApplicationBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<PeekLinkBotApiConfig>(builder.Configuration);

            builder.Services.ConfigureApplicationDatabase();

            builder.Services
                .AddControllers()
                .AddJsonOptions((options) =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
                });
        }
    }
}
