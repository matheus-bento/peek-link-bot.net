using MongoDB.Bson;
using PeekLinkBot.Data.Entities;

namespace PeekLinkBot.Data.Repositories
{
    public interface IBotInteractionRepository
    {
        /// <summary>
        ///     Returns all stored bot interactions
        /// </summary>
        Task<IEnumerable<BotInteraction>> GetAll();
        /// <summary>
        ///     Returns a bot interaction by its database id
        /// </summary>
        /// <param name="id">The id for the interaction in the database</param>
        Task<BotInteraction> GetById(ObjectId id);
        /// <summary>
        ///     Returns a bot interaction by the reddit id of the comment processed
        ///     by the bot
        /// </summary>
        /// <param name="redditId">The reddit id for the original comment</param>
        Task<BotInteraction> GetByRedditId(string redditId);
        /// <summary>
        ///     Saves a bot interaction to the database
        /// </summary>
        /// <param name="interaction">The interaction that will be saved</param>
        Task Save(BotInteraction interaction);
    }
}
