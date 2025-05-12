using MongoDB.Bson;
using PeekLinkBot.Data.Entities;

namespace PeekLinkBot.Data.Repositories
{
    public interface IBotInteractionRepository
    {
        Task<IEnumerable<BotInteraction>> GetAll();
        Task<BotInteraction> GetById(ObjectId id);
        Task Save(BotInteraction interaction);
    }
}
