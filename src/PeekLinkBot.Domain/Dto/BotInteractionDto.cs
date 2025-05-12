namespace PeekLinkBot.Domain.Dto
{
    public class BotInteractionDto
    {
        public string Id { get; set; }

        public CommentDto OriginalComment { get; set; }
        public CommentDto TriggerComment { get; set; }
        public CommentDto ReplyComment { get; set; }
    }
}
