namespace PeekLinkBot.Reddit.Api.Model
{
    /// <summary>
    ///    Class used to wrap comment information from reddit's API in json format
    /// </summary>
    public class RedditThing<T>
    {
        public string Kind { get; set; }
        public T Data { get; set; }
    }
}