using Microsoft.Extensions.Configuration;

namespace PeekLinkBot.Configuration
{
    public class PeekLinkBotConfig
    {
        /// <summary>
        ///     The bot's reddit account username
        /// </summary>
        [ConfigurationKeyName("USERNAME")]
        public string Username { get; set; }

        /// <summary>
        ///     The bot's reddit account password
        /// </summary>
        [ConfigurationKeyName("PASSWORD")]
        public string Password { get; set; }

        /// <summary>
        ///     The client ID generated after registering the application on the bot's reddit account.
        ///     Used in the authentication on the endpoint to retrieve the access token for reddit's API.
        /// </summary>
        [ConfigurationKeyName("CLIENT_ID")]
        public string ClientID { get; set; }

        /// <summary>
        ///     The secret generated after registering the application on the bot's reddit account.
        ///     Used in the authentication on the endpoint to retrieve the access token for reddit's API.
        /// </summary>
        [ConfigurationKeyName("SECRET")]
        public string Secret { get; set; }

        /// <summary>
        ///     The interval in seconds that between each inbox check
        /// </summary>
        [ConfigurationKeyName("MESSAGE_CHECK_INTERVAL")]
        public int MessageCheckInterval { get; set; }

        [ConfigurationKeyName("MONGO_DB_CONNECTION_STRING")]
        public string MongoDbConnectionString { get; set; }
    }
}
