using MongoDB.Bson;
using MongoDB.Driver;
using PeekLinkBot.Data.Entities;

namespace PeekLinkBot.Data.Repositories
{
    public class BotInteractionRepository : IBotInteractionRepository
    {
        private readonly IMongoCollection<BotInteraction> _collection;

        public BotInteractionRepository(IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase("peekLinkBot");
            this._collection = db.GetCollection<BotInteraction>("botInteractions");
        }

        public async Task<IEnumerable<BotInteraction>> GetAll()
        {
            var cursor = await this._collection.FindAsync(_ => true);
            return cursor.ToEnumerable();
        }

        public async Task<BotInteraction> GetById(ObjectId id)
        {
            var cursor = await this._collection.FindAsync(x => x.Id == id);
            return await cursor.FirstOrDefaultAsync();
        }

        public async Task Save(BotInteraction interaction)
        {
            await this._collection.InsertOneAsync(interaction);
        }
    }
}
