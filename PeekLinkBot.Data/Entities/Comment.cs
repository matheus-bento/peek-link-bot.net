namespace PeekLinkBot.Data.Entities
{
    /// <summary>
    ///     Represents a reddit comment
    /// </summary>
    public class Comment
    {
        public string RedditId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
