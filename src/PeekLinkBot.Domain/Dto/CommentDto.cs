namespace PeekLinkBot.Domain.Dto
{
    public class CommentDto
    {
        public string RedditId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
