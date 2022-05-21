using PeekLinkBot.Reddit.Api.Model;

using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

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

        public async Task<RedditJson<Listing<RedditJson<Message>>>> GetUnreadMessages()
        {
            HttpResponseMessage response = await this._redditHttpClient.GetAsync("/message/unread");

            this._logger.LogDebug("Request: {0}", response.RequestMessage);

            this._logger.LogDebug("Response: {0}", response);
            this._logger.LogDebug("Response Content: {0}", await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();

            var unreadMessages =
                JsonConvert.DeserializeObject<RedditJson<Listing<RedditJson<Message>>>>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    });

            return unreadMessages;
        }
    }
}
