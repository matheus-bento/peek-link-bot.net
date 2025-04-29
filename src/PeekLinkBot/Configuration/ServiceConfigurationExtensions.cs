using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PeekLinkBot.Data.Repositories;

namespace PeekLinkBot.Configuration
{
    public static class ServiceConfigurationExtensions
    {
        /// <summary>
        ///     Add the MongoDB client and repositories into the service collection.
        /// </summary>
        public static IServiceCollection ConfigureApplicationDatabase(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<PeekLinkBotConfig>>();
                return new MongoClient(options.Value.MongoDbConnectionString);
            });

            services.AddScoped<IBotInteractionRepository, BotInteractionRepository>();

            return services;
        }
    }
}
