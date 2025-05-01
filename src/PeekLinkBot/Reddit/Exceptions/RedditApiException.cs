using System;

namespace PeekLinkBot.Reddit.Exceptions
{
    public class RedditApiException : Exception
    {
        private readonly string _message;

        public RedditApiException(string message) : base(message)
        {
            this._message = message;
        }

        public override string Message => base.Message;
    }
}
