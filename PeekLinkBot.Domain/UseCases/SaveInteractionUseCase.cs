using PeekLinkBot.Data.Entities;
using PeekLinkBot.Data.Repositories;
using PeekLinkBot.Domain.Dto;
using PeekLinkBot.Domain.Exceptions;

namespace PeekLinkBot.Domain.UseCases
{
    public class SaveInteractionUseCase
    {
        private readonly IBotInteractionRepository _repository;

        public SaveInteractionUseCase(IBotInteractionRepository repository) =>
            this._repository = repository;

        public async Task Execute(BotInteractionDto interaction)
        {
            if (interaction == null)
            {
                throw new ArgumentNullException(nameof(interaction));
            }

            if (interaction.OriginalComment == null)
            {
                throw new CommentNotInformedException("original comment");
            }

            if (interaction.TriggerComment == null)
            {
                throw new CommentNotInformedException("trigger comment");
            }

            if (interaction.ReplyComment == null)
            {
                throw new CommentNotInformedException("reply comment");
            }

            await this._repository.Save(new BotInteraction
            {
                OriginalComment = new Comment
                {
                    RedditId = interaction.OriginalComment.RedditId,
                    Username = interaction.OriginalComment.Username,
                    CreatedAt = interaction.OriginalComment.CreatedAt,
                    Content = interaction.OriginalComment.Content
                },
                TriggerComment = new Comment
                {
                    RedditId = interaction.TriggerComment.RedditId,
                    Username = interaction.TriggerComment.Username,
                    CreatedAt = interaction.TriggerComment.CreatedAt,
                    Content = interaction.TriggerComment.Content
                },
                ReplyComment = new Comment
                {
                    RedditId = interaction.ReplyComment.RedditId,
                    Username = interaction.ReplyComment.Username,
                    CreatedAt = interaction.ReplyComment.CreatedAt,
                    Content = interaction.ReplyComment.Content
                }
            });
        }
    }
}
