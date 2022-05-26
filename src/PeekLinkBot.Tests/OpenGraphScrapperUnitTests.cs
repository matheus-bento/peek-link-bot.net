using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeekLinkBot.Scraper;
using PeekLinkBot.Scraper.Model;

namespace PeekLinkBot.Tests
{
    [TestClass]
    public class OpenGraphScrapperUnitTests
    {
        private UrlHandler _urlHandler;

        public OpenGraphScrapperUnitTests()
        {
            this._urlHandler = new UrlHandler();

            HtmlDocument dom = new HtmlWeb().LoadFromWebAsync("https://www.youtube.com/watch?v=dQw4w9WgXcQ").Result;
            this._urlHandler.AddScraper(new OpenGraphScraper(dom));
        }

        [TestMethod]
        public void ShouldExtractCorrtectOgTitle()
        {
            PageInfo info = this._urlHandler.GetPageInfo();
            Assert.AreEqual("Rick Astley - Never Gonna Give You Up (Official Music Video)", info.Title);
        }

        [TestMethod]
        public void ShouldExtractCorrtectOgType()
        {
            PageInfo info = this._urlHandler.GetPageInfo();
            Assert.AreEqual("video.other", info.Type);
        }

        [TestMethod]
        public void ShouldExtractCorrtectOgImage()
        {
            PageInfo info = this._urlHandler.GetPageInfo();
            Assert.AreEqual("https://i.ytimg.com/vi/dQw4w9WgXcQ/maxresdefault.jpg", info.Image);
        }

        [TestMethod]
        public void ShouldExtractCorrtectOgUrl()
        {
            PageInfo info = this._urlHandler.GetPageInfo();
            Assert.AreEqual("https://www.youtube.com/watch?v=dQw4w9WgXcQ", info.Url);
        }

        [TestMethod]
        public void ShouldExtractCorrtectOgDescription()
        {
            PageInfo info = this._urlHandler.GetPageInfo();
            Assert.AreEqual(
                @"The official video for “Never Gonna Give You Up” by Rick AstleyTaken from the album " +
                 "‘Whenever You Need Somebody’ – deluxe 2CD and digital deluxe out 6th May ...", info.Description);
        }
    }
}