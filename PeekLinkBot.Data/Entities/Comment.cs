using MongoDB.Bson;

namespace PeekLinkBot.Data.Entities
{
    /// <summary>
    ///     Represents a reddit comment
    /// </summary>
    public class Comment
    {
        public ObjectId Id { get; set; }

        public string RedditId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
