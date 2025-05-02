using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeekLinkBot.Reddit.Auth;

namespace PeekLinkBot.Tests
{
    [TestClass]
    public class RedditAuthTests
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private Mock<ILogger<PeekLinkBotService>> _mockLogger;

        [TestInitialize]
        public void Initialize()
        {
            this._mockHttpClientFactory = new Mock<IHttpClientFactory>();
            this._mockLogger = new Mock<ILogger<PeekLinkBotService>>();
        }

        [TestMethod]
        [TestCategory("RedditAuth")]
        public void RedditAuthShouldInitialize()
        {
            Assert.IsNotNull(
                new RedditAuth(
                    this._mockHttpClientFactory.Object,
                    this._mockLogger.Object,
                    "username",
                    "password",
                    "client_id",
                    "secret"
                )
            );
        }

        [TestMethod]
        [TestCategory("RedditAuth")]
        public void RedditAuthShouldThrowIfUsernameIsNull()
        {
            Assert.Throws<ArgumentException>(() =>
                new RedditAuth(
                    this._mockHttpClientFactory.Object,
                    this._mockLogger.Object,
                    null,
                    "password",
                    "client_id",
                    "secret"
                )
            );
        }

        [TestMethod]
        [TestCategory("RedditAuth")]
        public void RedditAuthShouldThrowIfPasswordIsNull()
        {
            Assert.Throws<ArgumentException>(() =>
                new RedditAuth(
                    this._mockHttpClientFactory.Object,
                    this._mockLogger.Object,
                    "username",
                    null,
                    "client_id",
                    "secret"
                )
            );
        }

        [TestMethod]
        [TestCategory("RedditAuth")]
        public void RedditAuthShouldThrowIfClientIdIsNull()
        {
            Assert.Throws<ArgumentException>(() =>
                new RedditAuth(
                    this._mockHttpClientFactory.Object,
                    this._mockLogger.Object,
                    "username",
                    "password",
                    null,
                    "secret"
                )
            );
        }

        [TestMethod]
        [TestCategory("RedditAuth")]
        public void RedditAuthShouldThrowIfSecretIsNull()
        {
            Assert.Throws<ArgumentException>(() =>
                new RedditAuth(
                    this._mockHttpClientFactory.Object,
                    this._mockLogger.Object,
                    "username",
                    "password",
                    "client_id",
                    null
                )
            );
        }
    }
}
