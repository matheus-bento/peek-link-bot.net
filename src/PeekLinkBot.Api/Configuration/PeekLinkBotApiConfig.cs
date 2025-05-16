using Microsoft.Extensions.Configuration;

namespace PeekLinkBot.Api.Configuration
{
    public class PeekLinkBotApiConfig
    {
        [ConfigurationKeyName("MONGO_DB_CONNECTION_STRING")]
        public string MongoDbConnectionString { get; set; }
    }
}
