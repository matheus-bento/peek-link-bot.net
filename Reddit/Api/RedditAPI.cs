using PeekLinkBot.Reddit.Api.Model;

using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;

namespace PeekLinkBot.Reddit.Api
{
    /// <summary>
    ///     Class that retrieves information from the bot's reddit account
    /// </summary>
    public class RedditAPI
    {
        private readonly ILogger<PeekLinkBotService> _logger;
        private readonly HttpClient _redditHttpClient;

        public RedditAPI(
            IHttpClientFactory httpClientFactory,
            ILogger<PeekLinkBotService> logger,
            string accessToken)
        {
            this._logger = logger;

            this._redditHttpClient = httpClientFactory.CreateClient("Reddit");
            this._redditHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async IAsyncEnumerable<Message> GetUnreadMessages()
        {
            int attempts = 0;
            int requestInterval = 1;

            while (true)
            {
                HttpResponseMessage response = await this._redditHttpClient.GetAsync("/message/unread");

                this._logger.LogDebug("Request: {0}", response.RequestMessage);

                this._logger.LogDebug("Response: {0}", response);
                this._logger.LogDebug("Response Content: {0}", await response.Content.ReadAsStringAsync());

                response.EnsureSuccessStatusCode();

                var unreadMessagesListing =
                    JsonConvert.DeserializeObject<RedditJson<Listing<RedditJson<Message>>>>(
                        await response.Content.ReadAsStringAsync(),
                        new JsonSerializerSettings
                        {
                            ContractResolver = new DefaultContractResolver
                            {
                                NamingStrategy = new SnakeCaseNamingStrategy()
                            }
                        });

                if (unreadMessagesListing.Data.Children.Count() > 0)
                {
                    foreach (RedditJson<Message> messageWrapper in unreadMessagesListing.Data.Children)
                    {
                        yield return messageWrapper.Data;
                    }
                }
                else
                {
                    Thread.Sleep(requestInterval * 1000);

                    if (requestInterval < 59)
                    {
                        requestInterval += (int)Math.Pow(2, attempts);
                    }

                    attempts++;
                }
            }
        }
    }
}
