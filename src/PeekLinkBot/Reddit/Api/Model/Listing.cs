using System.Collections.Generic;

namespace PeekLinkBot.Reddit.Api.Model
{
    /// <summary>
    ///    Class used to represent listing results returned from reddit's API
    /// </summary>
    public class Listing<T>
    {
        public string Before { get; set; }
        public string After { get; set; }
        public int Limit { get; set; }
        public int Count { get; set; }
        public string Show { get; set; }
        public IEnumerable<T> Children { get; set; }
    }
}