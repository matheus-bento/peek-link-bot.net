using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeekLinkBot.Scrapper;

namespace PeekLinkBot.Tests
{
    [TestClass]
    public class YoutubeScrapperUnitTests
    {

        [TestMethod]
        public async Task ShouldAcceptRegularYoutubeURL()
        {
            HtmlDocument dom = await new HtmlWeb().LoadFromWebAsync("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
            IScrapper scrapper = new YoutubeScrapper(dom);

            var expected = "# YouTube\n\n" +
                           "## [Rick Astley - Never Gonna Give You Up (Official Music Video)]" +
                           "(https://www.youtube.com/watch?v=dQw4w9WgXcQ) - [Rick Astley]" +
                           "(http://www.youtube.com/channel/UCuAXFkgsw1L7xaCfnd5JJOw)\n\n" +
                           "---";

            string actual = scrapper.GetPageInfo();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task ShouldAcceptShortenedYoutubeURL()
        {
            HtmlDocument dom = await new HtmlWeb().LoadFromWebAsync("https://youtu.be/dQw4w9WgXcQ");
            IScrapper scrapper = new YoutubeScrapper(dom);

            var expected = "# YouTube\n\n" +
                           "## [Rick Astley - Never Gonna Give You Up (Official Music Video)]" +
                           "(https://www.youtube.com/watch?v=dQw4w9WgXcQ) - [Rick Astley]" +
                           "(http://www.youtube.com/channel/UCuAXFkgsw1L7xaCfnd5JJOw)\n\n" +
                           "---";

            string actual = scrapper.GetPageInfo();

            Assert.AreEqual(expected, actual);
        }
    }
}