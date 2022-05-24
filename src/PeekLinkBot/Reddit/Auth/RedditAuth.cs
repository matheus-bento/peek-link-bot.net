using PeekLinkBot.Reddit.Auth.Model;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace PeekLinkBot.Reddit.Auth
{
    /// <summary>
    ///     Class with access to reddit's authentication endpoints using the client credentials grant type
    /// </summary>
    public class RedditAuth
    {
        private readonly ILogger<PeekLinkBotService> _logger;
        private readonly HttpClient _redditAuthHttpClient;

        private readonly string _username;
        private readonly string _password;

        public RedditAuth(
            IHttpClientFactory httpClientFactory,
            ILogger<PeekLinkBotService> logger,
            string username,
            string password,
            string clientID,
            string secret)
        {
            this._logger = logger;

            this._redditAuthHttpClient = httpClientFactory.CreateClient("RedditAuth");

            // Sets the Authorization header according to reddit's specification.
            // Reference: https://github.com/reddit-archive/reddit/wiki/OAuth2#retrieving-the-access-token
            byte[] credentialsBytes = Encoding.ASCII.GetBytes(clientID + ":" + secret);
            this._redditAuthHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentialsBytes));

            this._username = username;
            this._password = password;
        }

        /// <summary>
        ///     Retrieves the access token to access the bot account
        /// </summary>
        public async Task<AccessTokenResponseContent> GetAccessToken()
        {
            HttpResponseMessage response =
                await this._redditAuthHttpClient.PostAsync(
                    "/api/v1/access_token",
                    new FormUrlEncodedContent(
                        new Dictionary<string, string>
                        {
                            { "grant_type", "password" },
                            { "username", this._username },
                            { "password", this._password }
                        }
                    )
                );

            this._logger.LogDebug("Request: {0}", response.RequestMessage);
            this._logger.LogDebug("Request Content: {0}", await response.RequestMessage.Content.ReadAsStringAsync());

            this._logger.LogDebug("Response: {0}", response);
            this._logger.LogDebug("Response Content: {0}", await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();

            AccessTokenResponseContent content = 
                JsonConvert.DeserializeObject<AccessTokenResponseContent>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    }
                );

            return content;
        }
    }
}
