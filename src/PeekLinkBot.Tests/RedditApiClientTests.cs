using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeekLinkBot.Reddit.Api;
using PeekLinkBot.Reddit.Api.Model;
using PeekLinkBot.Reddit.Exceptions;
using PeekLinkBot.Tests.Core;

namespace PeekLinkBot.Tests;

[TestClass]
public class RedditApiClientTests
{
    private Mock<IHttpClientFactory> _mockHttpClientFactory;
    private Mock<ILogger<PeekLinkBotService>> _mockLogger;

    [TestInitialize]
    public void Initialize()
    {
        this._mockLogger = new Mock<ILogger<PeekLinkBotService>>();

        // IHttpClientFactory setup with MockBehavior.Strict so that it requires
        // Setup for the CreateClient method
        this._mockHttpClientFactory =
            new Mock<IHttpClientFactory>(MockBehavior.Strict);
    }

    [TestMethod]
    [TestCategory("GetMessageById")]
    [DataRow(HttpStatusCode.InternalServerError)]
    [DataRow(HttpStatusCode.Unauthorized)]
    [DataRow(HttpStatusCode.NotFound)]
    public async Task ThrowsHttpRequestExceptionIfGetMessageByIdIsUnsuccessful(
        HttpStatusCode statusCode)
    {
        HttpClient httpClient =
            new MockHttpClient()
                .Map(HttpMethod.Get, "/api/info", new HttpResponseMessage
                {
                    StatusCode = statusCode
                })
                .GetHttpClient();

        this._mockHttpClientFactory
            .Setup(factory => factory.CreateClient("Reddit"))
            .Returns(httpClient);

        var redditApi =
            new RedditAPI(
                this._mockHttpClientFactory.Object,
                this._mockLogger.Object,
                "unused"
            );

        await Assert.ThrowsAsync<HttpRequestException>(
            async () => await redditApi.GetMessageById("s0m31D"));
    }

    [TestMethod]
    [TestCategory("MarkMessageAsRead")]
    [DataRow(HttpStatusCode.InternalServerError)]
    [DataRow(HttpStatusCode.Unauthorized)]
    [DataRow(HttpStatusCode.NotFound)]
    public async Task ThrowsHttpRequestExceptionIfMarkMessageAsReadIsUnsuccessful(
        HttpStatusCode statusCode)
    {
        HttpClient httpClient =
            new MockHttpClient()
                .Map(HttpMethod.Post, "/api/read_message", new HttpResponseMessage
                {
                    StatusCode = statusCode
                })
                .GetHttpClient();

        this._mockHttpClientFactory
            .Setup(factory => factory.CreateClient("Reddit"))
            .Returns(httpClient);

        var loggerMock = new Mock<ILogger<PeekLinkBotService>>();

        var redditApi =
            new RedditAPI(
                this._mockHttpClientFactory.Object,
                this._mockLogger.Object,
                "unused"
            );

        await Assert.ThrowsAsync<HttpRequestException>(
            async () => await redditApi.MarkMessageAsRead(new Message()));
    }

    [TestMethod]
    [TestCategory("PostComment")]
    [DataRow(HttpStatusCode.InternalServerError)]
    [DataRow(HttpStatusCode.Unauthorized)]
    [DataRow(HttpStatusCode.NotFound)]
    public async Task ThrowsHttpRequestExceptionIfPostCommentIsUnsuccessful(
        HttpStatusCode statusCode)
    {
        HttpClient httpClient =
            new MockHttpClient()
                .Map(HttpMethod.Post, "/api/comment", new HttpResponseMessage
                {
                    StatusCode = statusCode
                })
                .GetHttpClient();

        this._mockHttpClientFactory
            .Setup(factory => factory.CreateClient("Reddit"))
            .Returns(httpClient);

        var loggerMock = new Mock<ILogger<PeekLinkBotService>>();

        var redditApi =
            new RedditAPI(
                this._mockHttpClientFactory.Object,
                this._mockLogger.Object,
                "unused"
            );

        await Assert.ThrowsAsync<HttpRequestException>(
            async () => await redditApi.PostComment("message_thing_id", "comment"));
    }

    [TestMethod]
    [TestCategory("GetUnreadMentions")]
    [DataRow(HttpStatusCode.InternalServerError)]
    [DataRow(HttpStatusCode.Unauthorized)]
    [DataRow(HttpStatusCode.NotFound)]
    public async Task ThrowsHttpRequestExceptionIfGetUnreadMentionsIsUnsuccessful(
        HttpStatusCode statusCode)
    {
        HttpClient httpClient =
            new MockHttpClient()
                .Map(HttpMethod.Get, "/message/unread", new HttpResponseMessage
                {
                    StatusCode = statusCode
                })
                .GetHttpClient();

        this._mockHttpClientFactory
            .Setup(factory => factory.CreateClient("Reddit"))
            .Returns(httpClient);

        var loggerMock = new Mock<ILogger<PeekLinkBotService>>();

        var redditApi =
            new RedditAPI(
                this._mockHttpClientFactory.Object,
                this._mockLogger.Object,
                "unused"
            );

        await Assert.ThrowsAsync<HttpRequestException>(redditApi.GetUnreadMentions);
    }
}
