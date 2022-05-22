using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PeekLinkBot.Reddit.Scrapper;

namespace PeekLinkBot.Reddit
{
    public static class CommentHandler
    {
        private static readonly Regex UrlRegex =
            new Regex(
                @"(?:https?:\/\/.)?(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b(?:[-a-zA-Z0-9@:%_\+.~#?&//=]*)"
            );

        private static readonly Regex YoutubeUrlRegex =
            new Regex(
                @"(?:http|https)://(?:(?:(?:www\.)?youtube.com)|youtu.be)(?:[/\w]*(?:\?(?:[\w\-_]+=[\w\-_]+)*)?)");

        /// <summary>
        ///     Gets informations about the links in the comment
        /// </summary>
        /// <param name="comment">Comment to be analysed</param>
        public static async Task<IEnumerable<string>> GetLinksInfo(string comment)
        {
            var linksInfo = new List<string>();

            foreach (Match match in CommentHandler.UrlRegex.Matches(comment))
            {
                string matchedUrl = match.Value;

                IScrapper scrapper = null;

                if (CommentHandler.YoutubeUrlRegex.IsMatch(matchedUrl))
                {
                    HtmlDocument dom = await new HtmlWeb().LoadFromWebAsync(matchedUrl);
                    scrapper = new YoutubeScrapper(dom);
                }

                if (scrapper != null)
                    linksInfo.Add(scrapper.GetPageInfo());
            }

            return linksInfo;
        }
    }
}