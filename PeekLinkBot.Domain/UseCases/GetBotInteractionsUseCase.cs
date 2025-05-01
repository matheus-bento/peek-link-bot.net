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
                        CreatedAtUtc = interaction.OriginalComment.CreatedAtUtc,
                        Content = interaction.OriginalComment.Content
                    },
                    TriggerComment = new CommentDto
                    {
                        RedditId = interaction.TriggerComment.RedditId,
                        Username = interaction.TriggerComment.Username,
                        CreatedAtUtc = interaction.TriggerComment.CreatedAtUtc,
                        Content = interaction.TriggerComment.Content
                    },
                    ReplyComment = new CommentDto
                    {
                        RedditId = interaction.ReplyComment.RedditId,
                        Username = interaction.ReplyComment.Username,
                        CreatedAtUtc = interaction.ReplyComment.CreatedAtUtc,
                        Content = interaction.ReplyComment.Content
                    }
                });

            return interactionsDto;
        }
    }
}
