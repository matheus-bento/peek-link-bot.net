using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeekLinkBot.Data.Repositories;
using PeekLinkBot.Domain.Dto;
using PeekLinkBot.Domain.Exceptions;
using PeekLinkBot.Domain.UseCases;
using PeekLinkBot.Tests.Core;

namespace PeekLinkBot.Tests
{
    [TestClass]
    public class SaveInteractionUseCaseTests
    {
        private IBotInteractionRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            this._repository = new MockBotInteractionRepository();
        }

        [TestMethod]
        [TestCategory("SaveInteractionUseCase")]
        public async Task ShouldNotSaveWithNullArgument()
        {
            var saveInteractionUseCase =
                new SaveInteractionUseCase(this._repository);

            await Assert.ThrowsExactlyAsync<ArgumentNullException>(
                () => saveInteractionUseCase.Execute(null));
        }

        [TestMethod]
        [TestCategory("SaveInteractionUseCase")]
        public async Task ShouldNotSaveWithoutOriginalComment()
        {
            var saveInteractionUseCase =
                new SaveInteractionUseCase(this._repository);

            var botInteraction = new BotInteractionDto
            {
                OriginalComment = null,
                TriggerComment = new CommentDto
                {
                    RedditId = "triggerId",
                    Username = "triggerUsername",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "triggerContent"
                },
                ReplyComment = new CommentDto
                {
                    RedditId = "replyId",
                    Username = "replyUsername",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "replyContent"
                }
            };

            await Assert.ThrowsExactlyAsync<CommentNotInformedException>(
                () => saveInteractionUseCase.Execute(botInteraction));
        }

        [TestMethod]
        [TestCategory("SaveInteractionUseCase")]
        public async Task ShouldNotSaveWithoutTriggerComment()
        {
            var saveInteractionUseCase =
                new SaveInteractionUseCase(this._repository);

            var botInteraction = new BotInteractionDto
            {
                OriginalComment = new CommentDto
                {
                    RedditId = "ogCommentId",
                    Username = "ogUsername",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "ogContent"
                },
                TriggerComment = null,
                ReplyComment = new CommentDto
                {
                    RedditId = "replyId",
                    Username = "replyUsername",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "replyContent"
                }
            };

            await Assert.ThrowsExactlyAsync<CommentNotInformedException>(
                () => saveInteractionUseCase.Execute(botInteraction));
        }

        [TestMethod]
        [TestCategory("SaveInteractionUseCase")]
        public async Task ShouldNotSaveWithoutReplyComment()
        {
            var saveInteractionUseCase =
                new SaveInteractionUseCase(this._repository);

            var botInteraction = new BotInteractionDto
            {
                OriginalComment = new CommentDto
                {
                    RedditId = "ogCommentId",
                    Username = "ogUsername",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "ogContent"
                },
                TriggerComment = new CommentDto
                {
                    RedditId = "triggerId",
                    Username = "triggerUsername",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "triggerContent"
                },
                ReplyComment = null
            };

            await Assert.ThrowsExactlyAsync<CommentNotInformedException>(
                () => saveInteractionUseCase.Execute(botInteraction));
        }

        [TestMethod]
        [TestCategory("SaveInteractionUseCase")]
        public async Task ShouldSaveInteraction()
        {
            var saveInteractionUseCase =
                new SaveInteractionUseCase(this._repository);

            var botInteraction = new BotInteractionDto
            {
                OriginalComment = new CommentDto
                {
                    RedditId = "ogCommentId",
                    Username = "ogUsername",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "ogContent"
                },
                TriggerComment = new CommentDto
                {
                    RedditId = "triggerId",
                    Username = "triggerUsername",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "triggerContent"
                },
                ReplyComment = new CommentDto
                {
                    RedditId = "replyId",
                    Username = "replyUsername",
                    CreatedAtUtc = DateTime.UtcNow,
                    Content = "replyContent"
                }
            };

            await saveInteractionUseCase.Execute(botInteraction);
        }
    }
}
