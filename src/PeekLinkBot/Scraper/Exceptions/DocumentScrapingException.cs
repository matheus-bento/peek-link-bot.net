using System;

namespace PeekLinkBot.Scraper.Exceptions
{
    public class DocumentScrapingException : Exception
    {
        public DocumentScrapingException() : base() {}
        public DocumentScrapingException(string message) : base(message) {}
        public DocumentScrapingException(string message, Exception innerException) : base(message, innerException) {}
    }
}