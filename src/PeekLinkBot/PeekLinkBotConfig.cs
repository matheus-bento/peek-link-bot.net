namespace PeekLinkBot
{
    public class PeekLinkBotConfig
    {
        /// <summary>
        ///     The bot's reddit account username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        ///     The bot's reddit account password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        ///     The client ID generated after registering the application on the bot's reddit account.
        ///     Used in the authentication on the endpoint to retrieve the access token for reddit's API.
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        ///     The secret generated after registering the application on the bot's reddit account.
        ///     Used in the authentication on the endpoint to retrieve the access token for reddit's API.
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        ///     The interval in seconds that between each inbox check
        /// </summary>
        public int MessageCheckInterval { get; set; }
    }
}
