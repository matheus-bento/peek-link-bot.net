namespace PeekLinkBot.Domain.Dto
{
    public class CommentDto
    {
        public string Id { get; set; }

        public string RedditId { get; set; }
        public string CreatedAt { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
