using System;
using Newtonsoft.Json;
using PeekLinkBot.Reddit.Api.Converters;

namespace PeekLinkBot.Reddit.Api.Model
{
    /// <summary>
    ///     Represents an inbox message
    /// </summary>
    public class Message
    {
        public string Id { get; set; }
        /// <summary>
        ///    fullname for the parent message
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        ///    fullname for the message
        /// </summary>
        public string Name { get; set; }
        public string Subject { get; set; }
        /// <summary>
        ///    Defines whether or not the message is a comment reply
        /// </summary>
        public bool WasComment { get; set; }

        public string Author { get; set; }
        public string Body { get; set; }
        [JsonConverter(typeof(RedditUnixTimestampConverter))]
        public DateTime? CreatedUtc { get; set; }
        public string Dest { get; set; }
    }
}