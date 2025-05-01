using PeekLinkBot.Reddit.Api.Model;

using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System;
using PeekLinkBot.Reddit.Exceptions;

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

        public async Task MarkMessageAsRead(Message message)
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
                JsonConvert.DeserializeObject<RedditThing<Listing<RedditThing<Message>>>>(
                    await response.Content.ReadAsStringAsync());

            if (messageListing.Data.Children.Count() == 0)
            {
                throw new RedditApiException("Message not found");
            }

            Message message = messageListing.Data.Children.First().Data;

            // reddit places backslashes before some characters in URLs
            // so we have to remove those before working with them
            message.Body = message.Body.Replace("\\", "");

            return message;
        }

        public async Task<Message> PostComment(string repliedMessageFullname, string text)
        {
            HttpResponseMessage response =
                await this._redditHttpClient.PostAsync(
                    "/api/comment",
                    new FormUrlEncodedContent(
                        new Dictionary<string, string>
                        {
                            { "api_type", "json" },
                            { "text", text },
                            { "thing_id", repliedMessageFullname },
                        }
                    ));

            this._logger.LogDebug("Request: {0}", response.RequestMessage);
            this._logger.LogDebug("Request Content: {0}", await response.RequestMessage.Content.ReadAsStringAsync());
            this._logger.LogDebug("Response: {0}", response);

            response.EnsureSuccessStatusCode();

            var messageListing =
                JsonConvert.DeserializeObject<RedditJson<Message>>(
                    await response.Content.ReadAsStringAsync());

            if (messageListing.Json.Data.Things.Count() == 0)
            {
                throw new RedditApiException("Did not receive data from reddit about the posted comment");
            }

            Message message = messageListing.Json.Data.Things.First().Data;

            // reddit places backslashes before some characters in URLs
            // so we have to remove those before working with them
            message.Body = message.Body.Replace("\\", "");

            return message;
        }

        public async Task<IEnumerable<Message>> GetUnreadMentions()
        {
            HttpResponseMessage response = await this._redditHttpClient.GetAsync("/message/unread");

            this._logger.LogDebug("Request: {0}", response.RequestMessage);

            this._logger.LogDebug("Response: {0}", response);
            this._logger.LogDebug("Response Content: {0}", await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();

            var unreadMessagesListing =
                JsonConvert.DeserializeObject<RedditThing<Listing<RedditThing<Message>>>>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    });

            var unreadMentionsJsonWrapper =
                unreadMessagesListing.Data.Children.Where(json => json.Data.Type == "username_mention");

            return unreadMentionsJsonWrapper.Select(json => json.Data);
        }
    }
}
