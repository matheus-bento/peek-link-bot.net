using System;

namespace PeekLinkBot.Reddit.Scrapper.Exceptions
{
    public class DocumentScrapingException : Exception
    {
        public DocumentScrapingException() : base() {}
        public DocumentScrapingException(string message) : base(message) {}
        public DocumentScrapingException(string message, Exception innerException) : base(message, innerException) {}
    }
}