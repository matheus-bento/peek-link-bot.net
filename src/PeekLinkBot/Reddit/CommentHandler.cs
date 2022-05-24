using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PeekLinkBot.Scrapper;

namespace PeekLinkBot.Reddit
{
    public static class CommentHandler
    {
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

            foreach (Match ytMatch in CommentHandler.YoutubeUrlRegex.Matches(comment))
            {
                string ytMatchedUrl = ytMatch.Value;

                HtmlDocument dom = await new HtmlWeb().LoadFromWebAsync(ytMatchedUrl);
                IScrapper scrapper = new YoutubeScrapper(dom);

                linksInfo.Add(scrapper.GetPageInfo());
            }

            return linksInfo;
        }
    }
}