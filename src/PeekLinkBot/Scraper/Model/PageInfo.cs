namespace PeekLinkBot.Scraper.Model
{
    /// <summary>
    ///     Represents the info our scrappers might extract from a page.
    ///     Most of this info is based off of the Open Graph protocol,
    ///     see https://ogp.me/ for reference about the available metadata
    /// </summary>
    public class PageInfo
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }

        // Optional metadata

        /// <summary>
        ///     A URL to an audio file to accompany this object
        /// </summary>
        public string Audio { get; set; }
        public string Description { get; set; }
        public string SiteName { get; set; }

    }
}