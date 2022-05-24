using System;
using HtmlAgilityPack;
using PeekLinkBot.Reddit.Scrapper.Exceptions;

namespace PeekLinkBot.Reddit.Scrapper
{
    /// <summary>
    ///     Class capable of scrapping information about a given youtube video
    /// </summary>
    public class YoutubeScrapper : IScrapper
    {
        private readonly HtmlDocument _dom;

        public YoutubeScrapper(HtmlDocument dom)
        {
            this._dom = dom;
        }

        public string GetPageInfo()
        {
            try
            {
                string videoUrl = this._dom.DocumentNode.SelectSingleNode("//link[@itemprop='url']").Attributes["href"].Value;
                string videoTitle = this._dom.DocumentNode.SelectSingleNode("//meta[@name='title']").Attributes["content"].Value;

                string channelName =
                    this._dom.DocumentNode
                        .SelectSingleNode("//span[@itemprop='author']/link[@itemprop='name']")
                        .Attributes["content"].Value;
                string channelUrl =
                    this._dom.DocumentNode
                        .SelectSingleNode("//span[@itemprop='author']/link[@itemprop='url']")
                        .Attributes["href"].Value;

                return String.Format(
                    "# YouTube\n\n" +
                    "## [{0}]({1}) - [{2}]({3})\n\n" +
                    "---", videoTitle, videoUrl, channelName, channelUrl);
            }
            catch (Exception ex)
            {
                throw new DocumentScrapingException("An error occurred while scrapping the document", ex);
            }
        }
    }
}