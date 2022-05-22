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

        private async Task MarkMessageAsRead(Message message)
        {
            HttpResponseMessage response =
                await this._redditHttpClient.PostAsync(
                    "/api/read_message",
                    new FormUrlEncodedContent(
                        new Dictionary<string, string>
                        {
                            { "id", message.Name }
                        }
                    ));

            this._logger.LogDebug("Request: {0}", response.RequestMessage);
            this._logger.LogDebug("Request Content: {0}", await response.RequestMessage.Content.ReadAsStringAsync());
            this._logger.LogDebug("Response: {0}", response);

            response.EnsureSuccessStatusCode();
        }

        public async Task<Message> GetMessageById(string messageId)
        {
            HttpResponseMessage response =
                await this._redditHttpClient.GetAsync(
                    String.Format("/api/info?id={0}", messageId));

            this._logger.LogDebug("Request: {0}", response.RequestMessage);

            this._logger.LogDebug("Response: {0}", response);
            this._logger.LogDebug("Response Content: {0}", await response.Content.ReadAsStringAsync());

            var messageListing =
                JsonConvert.DeserializeObject<RedditJson<Listing<RedditJson<Message>>>>(
                        await response.Content.ReadAsStringAsync());

            if (messageListing.Data.Children.Count() > 0)
            {
                Message message = messageListing.Data.Children.First().Data;
                return message;
            }
            else
            {
                return null;
            }
        }

        public async IAsyncEnumerable<Message> GetUnreadMentions()
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

                var unreadMentions =
                    unreadMessagesListing.Data.Children.Where(json => json.Data.Type == "username_mention");

                if (unreadMentions.Count() > 0)
                {
                    foreach (RedditJson<Message> messageWrapper in unreadMentions)
                    {
                        Message message = messageWrapper.Data;
                        await MarkMessageAsRead(message);

                        yield return message;
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
