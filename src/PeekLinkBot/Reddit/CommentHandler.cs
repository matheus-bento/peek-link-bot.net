using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PeekLinkBot.Scraper;
using PeekLinkBot.Scraper.Model;

namespace PeekLinkBot.Reddit
{
    public class CommentHandler
    {
        /// <summary>
        ///     Regex to identify any URL
        /// </summary>
        private readonly Regex URL_REGEX = new Regex(
            @"(?:https?:\/\/)?(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-z]{2,6}\b(?:[-a-zA-Z0-9@:%_\+.~#?&;//=]*)");

        private ILogger<PeekLinkBotService> _logger;
        private readonly string _comment;

        public CommentHandler(ILogger<PeekLinkBotService> logger, string comment)
        {
            this._logger = logger;
            this._comment = comment;
        }

        /// <summary>
        ///     Identifies all URLs in the comment an returns an IEnumerable with a page info string for each URL
        /// </summary>
        public async Task<IEnumerable<string>> GetUrlInfo()
        {
            var urlInfo = new List<string>();

            IEnumerable<string> urls = (from m in this.URL_REGEX.Matches(this._comment)
                                        select m.Value.Replace("\\", "")).Distinct();

            foreach (string url in urls)
            {
                var handler = new UrlHandler();

                var htmlWeb = new HtmlWeb
                {
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36"
                };

                HtmlDocument dom = await htmlWeb.LoadFromWebAsync(url);

                handler.AddScraper(new OpenGraphScraper(dom));

                PageInfo info = handler.GetPageInfo();

                string infoString = this.GetUrlInfoString(info);

                this._logger.LogDebug("Info string for '{0}': {1}", url, infoString);

                if (!String.IsNullOrEmpty(infoString))
                {
                    urlInfo.Add(infoString);
                }

            }

            return urlInfo;
        }

        private string GetUrlInfoString(PageInfo info)
        {
            string infoString = "";

            if (!String.IsNullOrEmpty(info.SiteName))
            {
                infoString += String.Format("# {0}\n\n", info.SiteName);
            }

            if (!String.IsNullOrEmpty(info.Title))
            {
                if (!String.IsNullOrEmpty(info.Url))
                {
                    infoString += String.Format("## [{0}]({1})\n\n", info.Title, info.Url);
                }
                else
                {
                    infoString += String.Format("## {0}\n\n", info.Title);
                }
            }
            else if (!String.IsNullOrEmpty(info.Url))
            {
                infoString += String.Format("## Original link: {0}\n\n", info.Url);
            }

            if (!String.IsNullOrEmpty(info.Description))
            {
                string description = info.Description;

                // Truncating the description to avoid long texts
                if (info.Description.Length > 180)
                    description = info.Description.Substring(0, 180) + "...";

                infoString += String.Format("{0}\n\n", description);
            }

            // If the info string didn't catch any metadata don't add the separator so
            // that the service can identify that no metadata was found for this website
            if (!String.IsNullOrEmpty(infoString))
                infoString += "---\n";

            return infoString;
        }
    }
}