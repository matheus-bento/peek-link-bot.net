using PeekLinkBot.Reddit.Api.Model;

using System;
using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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

        /// <summary>
        ///     Retrieves informations about the bot's account
        /// </summary>
        public async Task<RedditUserIdentity> GetAccountInfo()
        {
            HttpResponseMessage response = await this._redditHttpClient.GetAsync("/api/v1/me");

            this._logger.LogDebug("Request: {0}", response.RequestMessage);
            
            this._logger.LogDebug("Response: {0}", response);
            this._logger.LogDebug("Response Content: {0}", await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();

            var acctInfo =
                JsonConvert.DeserializeObject<RedditUserIdentity>(
                    response.Content.ReadAsStringAsync().Result,
                    new JsonSerializerSettings 
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    });

            return acctInfo;
        }
    }
}
