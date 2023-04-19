using System.Collections.Generic;
using PeekLinkBot.Scraper.Model;

namespace PeekLinkBot.Scraper
{
	/// <summary>
	///     A class that applies some scrapers to extract metadata from a URL
	/// </summary>
	public class UrlHandler
    {
        /// <summary>
        ///     The list of scrapers that will be executed on this page
        /// </summary>
        private IList<IScraper> _scrapers = new List<IScraper>();

        public UrlHandler() {}

        public void AddScraper(IScraper scraper)
        {
            this._scrapers.Add(scraper);
        }

        public PageInfo GetPageInfo()
        {
            PageInfo info = new PageInfo();

            foreach (IScraper scraper in this._scrapers)
            {
                info = scraper.Scrape(info);
            }

            return info;
        }
    }
}