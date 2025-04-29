using PeekLinkBot.Data.Repositories;
using PeekLinkBot.Domain.Dto;

namespace PeekLinkBot.Domain.UseCases
{
    public class GetBotInteractionsUseCase
    {
        private readonly IBotInteractionRepository _repository;

        public GetBotInteractionsUseCase(IBotInteractionRepository repository) =>
            this._repository = repository;

        public async Task<IEnumerable<BotInteractionDto>> Execute()
        {
            var interactions = await this._repository.GetAll();

            var interactionsDto =
                interactions.Select(interaction => new BotInteractionDto
                {
                    Id = interaction.Id.ToString(),
                    OriginalComment = new CommentDto
                    {
                        RedditId = interaction.OriginalComment.RedditId,
                        Username = interaction.OriginalComment.Username,
                        CreatedAt = interaction.OriginalComment.CreatedAt.ToString(),
                        Content = interaction.OriginalComment.Content
                    },
                    TriggerComment = new CommentDto
                    {
                        RedditId = interaction.TriggerComment.RedditId,
                        Username = interaction.TriggerComment.Username,
                        CreatedAt = interaction.TriggerComment.CreatedAt.ToString(),
                        Content = interaction.TriggerComment.Content
                    },
                    ReplyComment = new CommentDto
                    {
                        RedditId = interaction.ReplyComment.RedditId,
                        Username = interaction.ReplyComment.Username,
                        CreatedAt = interaction.ReplyComment.CreatedAt.ToString(),
                        Content = interaction.ReplyComment.Content
                    }
                });

            return interactionsDto;
        }
    }
}
