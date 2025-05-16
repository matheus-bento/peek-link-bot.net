using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using PeekLinkBot.Data.Entities;
using PeekLinkBot.Data.Repositories;

namespace PeekLinkBot.Tests.Core
{
    public class MockBotInteractionRepository : IBotInteractionRepository
    {
        private IList<BotInteraction> _interactions;

        public MockBotInteractionRepository()
        {
            this._interactions = new List<BotInteraction>();
        }

        public Task<IEnumerable<BotInteraction>> GetAll()
        {
            return Task.FromResult(this._interactions.AsEnumerable());
        }

        public Task<BotInteraction> GetById(ObjectId id)
        {
            return Task.FromResult(this._interactions.FirstOrDefault(i => i.Id == id));
        }

        public Task<BotInteraction> GetByRedditId(string redditId)
        {
            return Task.FromResult(this._interactions.FirstOrDefault(i => i.OriginalComment.RedditId == redditId));
        }

        public Task Save(BotInteraction interaction)
        {
            this._interactions.Add(interaction);
            return Task.CompletedTask;
        }
    }
}
