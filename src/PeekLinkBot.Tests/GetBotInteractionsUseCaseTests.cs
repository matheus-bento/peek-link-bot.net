using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeekLinkBot.Data.Repositories;
using PeekLinkBot.Domain.Dto;
using PeekLinkBot.Domain.UseCases;
using PeekLinkBot.Tests.Core;

namespace PeekLinkBot.Tests
{
    [TestClass]
    public class GetBotInteractionsUseCaseTests
    {
        private IBotInteractionRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            this._repository = new MockBotInteractionRepository();
        }

        [TestMethod]
        [TestCategory("GetBotInteractionsUseCase")]
        public async Task ShouldNotReturnNullWhenNoInteractions()
        {
            var getBotInteractionsUseCase =
                new GetBotInteractionsUseCase(this._repository);

            IEnumerable<BotInteractionDto> botInteractions =
                await getBotInteractionsUseCase.Execute();

            Assert.IsNotNull(botInteractions);
        }

        [TestMethod]
        [TestCategory("GetBotInteractionsUseCase")]
        public async Task ShouldReturnEmptyListWhenNoInteractions()
        {
            var getBotInteractionsUseCase =
                new GetBotInteractionsUseCase(this._repository);

            IEnumerable<BotInteractionDto> botInteractions =
                await getBotInteractionsUseCase.Execute();

            Assert.IsEmpty(botInteractions);
        }


        [TestMethod]
        [TestCategory("GetBotInteractionsUseCase")]
        public async Task ShouldReturnStoredInteractions()
        {
            var saveInteractionUseCase =
                new SaveInteractionUseCase(this._repository);

            var creationDate = DateTime.UtcNow;

            await saveInteractionUseCase.Execute(new BotInteractionDto
            {
                OriginalComment = new CommentDto
                {
                    RedditId = "ogCommentId",
                    Username = "ogUsername",
                    CreatedAtUtc = creationDate,
                    Content = "ogContent"
                },
                TriggerComment = new CommentDto
                {
                    RedditId = "triggerId",
                    Username = "triggerUsername",
                    CreatedAtUtc = creationDate,
                    Content = "triggerContent"
                },
                ReplyComment = new CommentDto
                {
                    RedditId = "replyId",
                    Username = "replyUsername",
                    CreatedAtUtc = creationDate,
                    Content = "replyContent"
                }
            });

            var getBotInteractionsUseCase =
                new GetBotInteractionsUseCase(this._repository);

            IEnumerable<BotInteractionDto> botInteractions =
                await getBotInteractionsUseCase.Execute();

            Assert.ContainsSingle(botInteractions);

            BotInteractionDto botInteraction = botInteractions.FirstOrDefault();

            Assert.IsNotNull(botInteraction.Id);

            Assert.AreEqual("ogCommentId", botInteraction.OriginalComment.RedditId);
            Assert.AreEqual("ogUsername", botInteraction.OriginalComment.Username);
            Assert.AreEqual(creationDate, botInteraction.OriginalComment.CreatedAtUtc);
            Assert.AreEqual("ogContent", botInteraction.OriginalComment.Content);

            Assert.AreEqual("triggerId", botInteraction.TriggerComment.RedditId);
            Assert.AreEqual("triggerUsername", botInteraction.TriggerComment.Username);
            Assert.AreEqual(creationDate, botInteraction.TriggerComment.CreatedAtUtc);
            Assert.AreEqual("triggerContent", botInteraction.TriggerComment.Content);

            Assert.AreEqual("replyId", botInteraction.ReplyComment.RedditId);
            Assert.AreEqual("replyUsername", botInteraction.ReplyComment.Username);
            Assert.AreEqual(creationDate, botInteraction.ReplyComment.CreatedAtUtc);
            Assert.AreEqual("replyContent", botInteraction.ReplyComment.Content);
        }
    }
}
