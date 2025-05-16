using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeekLinkBot.Data.Repositories;
using PeekLinkBot.Domain.Dto;
using PeekLinkBot.Domain.UseCases;
using PeekLinkBot.Tests.Core;

namespace PeekLinkBot.Tests
{
    [TestClass]
    public class GetBotInteractionByRedditIdUseCaseTests
    {
        private IBotInteractionRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            this._repository = new MockBotInteractionRepository();
        }

        [TestMethod]
        [TestCategory("GetBotInteractionByRedditIdUseCase")]
        public async Task ShouldThrowIfRedditIdIsNull()
        {
            var getBotInteractionByRedditIdUseCase =
                new GetBotInteractionByRedditIdUseCase(this._repository);

            await Assert.ThrowsAsync<ArgumentException>(
                () => getBotInteractionByRedditIdUseCase.Execute(null));
        }

        [TestMethod]
        [TestCategory("GetBotInteractionByRedditIdUseCase")]
        public async Task ShouldThrowIfRedditIdIsEmpty()
        {
            var getBotInteractionByRedditIdUseCase =
                new GetBotInteractionByRedditIdUseCase(this._repository);
            await Assert.ThrowsAsync<ArgumentException>(
                () => getBotInteractionByRedditIdUseCase.Execute(string.Empty));
        }

        [TestMethod]
        [TestCategory("GetBotInteractionByRedditIdUseCase")]
        public async Task ShouldThrowIfRedditIdIsWhitespace()
        {
            var getBotInteractionByRedditIdUseCase =
                new GetBotInteractionByRedditIdUseCase(this._repository);
            await Assert.ThrowsAsync<ArgumentException>(
                () => getBotInteractionByRedditIdUseCase.Execute(" "));
        }

        [TestMethod]
        [TestCategory("GetBotInteractionByRedditIdUseCase")]
        public async Task ShouldGetInteraction()
        {
            var saveInteractionUseCase =
                new SaveInteractionUseCase(this._repository);

            await saveInteractionUseCase.Execute(new BotInteractionDto
            {
                OriginalComment = new CommentDto
                {
                    RedditId = "redditId1",
                    Username = "user1",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "content1"
                },
                TriggerComment = new CommentDto
                {
                    RedditId = "redditId2",
                    Username = "user2",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "content2"
                },
                ReplyComment = new CommentDto
                {
                    RedditId = "redditId3",
                    Username = "user3",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "content3"
                }
            });

            var getBotInteractionByRedditIdUseCase =
                new GetBotInteractionByRedditIdUseCase(this._repository);

            BotInteractionDto interaction =
                await getBotInteractionByRedditIdUseCase.Execute("redditId1");

            Assert.IsNotNull(interaction);
        }
    }
}
