using MongoDB.Bson;

namespace PeekLinkBot.Data.Entities
{
    public class BotInteraction
    {
        public ObjectId Id { get; set; }

        /// <summary>
        ///     The comment processed by the bot
        /// </summary>
        public Comment OriginalComment { get; set; }

        /// <summary>
        ///     The comment that triggered the bot by u/ mentioning it
        ///     below the original comment
        /// </summary>
        public Comment TriggerComment { get; set; }

        /// <summary>
        ///     The comment that made by the bot to reply the trigger comment
        /// </summary>
        public Comment ReplyComment { get; set; }
    }
}
