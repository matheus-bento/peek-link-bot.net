using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace PeekLinkBot.Tests.Core
{
    /// <summary>
    ///     Class that provides a <see cref="HttpClient"/> with mocked endpoints.
    /// </summary>
    public class MockHttpClient
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

        public MockHttpClient()
        {
            this._httpMessageHandlerMock =
                new Mock<HttpMessageHandler>(MockBehavior.Strict);
        }

        /// <summary>
        ///     <para>
        ///         Maps the given method and path to the given response in the mock
        ///         HttpMessageHandler that will be returend by the
        ///         <see cref="MockHttpClient.GetHttpClient()"/> method.
        ///     </para>
        ///     
        ///     <para>
        ///         Returns the <see cref="MockHttpClient"/> instance to enable chaining.
        ///     </para>
        /// </summary>
        /// <param name="method">HTTP method for the request</param>
        /// <param name="path">Path for the requested resource</param>
        /// <param name="res">The response object</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public MockHttpClient Map(
            HttpMethod method, string path, HttpResponseMessage res)
        {
            ArgumentNullException.ThrowIfNull(method);
            ArgumentException.ThrowIfNullOrEmpty(path);
            ArgumentNullException.ThrowIfNull(res);

            this._httpMessageHandlerMock
                .Protected()
                .Setup<HttpResponseMessage>(
                    "Send",
                    ItExpr.Is<HttpRequestMessage>(
                        req => req.Method == method &&
                               req.RequestUri.AbsolutePath == path),
                    ItExpr.IsAny<CancellationToken>()
                )
                .Returns(res);

            this._httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(
                        req => req.Method == method &&
                               req.RequestUri.AbsolutePath == path),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(res);

            return this;
        }

        /// <summary>
        ///     Returns a new instance of <see cref="HttpClient"/> with the mocked
        ///     endpoints set up in the
        ///     <see cref="MockHttpClient.Map(HttpMethod, string, HttpResponseMessage)"/>
        ///     method.
        /// </summary>
        public HttpClient GetHttpClient()
        {
            return new HttpClient(this._httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://test.dev")
            };
        }
    }
}
