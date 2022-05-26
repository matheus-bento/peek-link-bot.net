using HtmlAgilityPack;
using PeekLinkBot.Scraper.Model;

namespace PeekLinkBot.Scraper
{
    /// <summary>
    ///     Interface that defines a method capable of scraping a certain document and extract some metadata from it
    /// </summary>
    public interface IScraper
    {
        /// <summary>
        ///     Scrapes a web page and returns a PageInfo with metadata describing it
        /// </summary>
        /// <param name="info">The object containing page info that will be filled by the scraper</param>
        PageInfo Scrape(PageInfo info);
    }
}