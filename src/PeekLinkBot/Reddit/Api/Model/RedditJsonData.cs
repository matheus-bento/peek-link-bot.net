using System.Collections.Generic;

namespace PeekLinkBot.Reddit.Api.Model
{
    public class RedditJsonData<T>
    {
        public IEnumerable<RedditThing<T>> Things { get; set; }
    }
}
