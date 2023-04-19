using System;
using HtmlAgilityPack;
using PeekLinkBot.Scraper.Exceptions;
using PeekLinkBot.Scraper.Model;

namespace PeekLinkBot.Scraper
{
    /// <summary>
    ///     Class capable of scrapping information about a given youtube video
    /// </summary>
    public class OpenGraphScraper : IScraper
    {
        /// <summary>
        ///     A representation of the DOM that will be used to extract the metadata
        /// </summary>
        private HtmlDocument _dom;

        public OpenGraphScraper(HtmlDocument dom)
        {
            this._dom = dom;
        }

        public PageInfo Scrape(PageInfo info)
        {
            try
            {
                if (String.IsNullOrEmpty(info.Title))
                {
                    HtmlNode ogTitle = this._dom.DocumentNode.SelectSingleNode("//meta[@property='og:title']");

                    if (ogTitle != null)
                        info.Title = ogTitle.Attributes["content"].Value;
                }

                if (String.IsNullOrEmpty(info.Type))
                {
                    HtmlNode ogType = this._dom.DocumentNode.SelectSingleNode("//meta[@property='og:type']");

                    if (ogType != null)
                        info.Type = ogType.Attributes["content"].Value;
                }

                if (String.IsNullOrEmpty(info.Image))
                {
                    HtmlNode ogImage = this._dom.DocumentNode.SelectSingleNode("//meta[@property='og:image']");

                    if (ogImage != null)
                        info.Image = ogImage.Attributes["content"].Value;
                }

                if (String.IsNullOrEmpty(info.Url))
                {
                    HtmlNode ogUrl = this._dom.DocumentNode.SelectSingleNode("//meta[@property='og:url']");
    
                    if (ogUrl != null)
                        info.Url = ogUrl.Attributes["content"].Value;
                }

                if (String.IsNullOrEmpty(info.Audio))
                {
                    HtmlNode ogAudio = this._dom.DocumentNode.SelectSingleNode("//meta[@property='og:audio']");

                    if (ogAudio != null)
                        info.Audio = ogAudio.Attributes["content"].Value;
                }

                if (String.IsNullOrEmpty(info.Description))
                {
                    HtmlNode ogDescription =
                        this._dom.DocumentNode.SelectSingleNode("//meta[@property='og:description']");

                    if (ogDescription != null)
                        info.Description = ogDescription.Attributes["content"].Value;
                }

                if (String.IsNullOrEmpty(info.SiteName))
                {
                    HtmlNode ogSiteName = this._dom.DocumentNode.SelectSingleNode("//meta[@property='og:site_name']");

                    if (ogSiteName != null)
                        info.SiteName = ogSiteName.Attributes["content"].Value;
                }

                return info;
            }
            catch (Exception ex)
            {
                throw new DocumentScrapingException("An error occurred while scrapping the document", ex);
            }
        }
    }
}