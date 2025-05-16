using PeekLinkBot.Data.Repositories;
using PeekLinkBot.Domain.Dto;

namespace PeekLinkBot.Domain.UseCases
{
    public class GetBotInteractionByRedditIdUseCase
    {
        private readonly IBotInteractionRepository _botInteractionRepository;

        public GetBotInteractionByRedditIdUseCase(IBotInteractionRepository botInteractionRepository)
        {
            this._botInteractionRepository = botInteractionRepository;
        }

        public async Task<BotInteractionDto> Execute(string redditId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(redditId, nameof(redditId));

            var interaction = await this._botInteractionRepository.GetByRedditId(redditId);

            return new BotInteractionDto
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
            };
        }
    }
}
