namespace PeekLinkBot.Reddit.Api.Model
{
    /// <summary>
    ///    Class used to wrap listing results returned from reddit's API in json format
    /// </summary>
    public class RedditJson<T>
    {
        public string Kind { get; set; }
        public T Data { get; set; }
    }
}