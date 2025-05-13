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
using PeekLinkBot.Configuration;
using PeekLinkBot.Reddit.Model;

namespace PeekLinkBot.Reddit.Auth
{
    /// <summary>
    ///     Class with access to reddit's authentication endpoints using the client credentials grant type
    /// </summary>
    public class RedditAuth
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PeekLinkBotService> _logger;

        private readonly string _username;
        private readonly string _password;
        private readonly string _clientID;
        private readonly string _secret;

        public RedditAuth(
            IHttpClientFactory httpClientFactory,
            ILogger<PeekLinkBotService> logger,
            string username,
            string password,
            string clientID,
            string secret)
        {
            ArgumentException.ThrowIfNullOrEmpty(username);
            ArgumentException.ThrowIfNullOrEmpty(password);
            ArgumentException.ThrowIfNullOrEmpty(clientID);
            ArgumentException.ThrowIfNullOrEmpty(secret);

            this._httpClientFactory = httpClientFactory;
            this._logger = logger;

            this._username = username;
            this._password = password;
            this._clientID = clientID;
            this._secret = secret;
        }

        private HttpClient GetHttpClient()
        {
            HttpClient httpClient = this._httpClientFactory.CreateClient("RedditAuth");

            // Sets the Authorization header according to reddit's specification.
            // Reference: https://github.com/reddit-archive/reddit/wiki/OAuth2#retrieving-the-access-token
            byte[] credentialsBytes = Encoding.ASCII.GetBytes(this._clientID + ":" + this._secret);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentialsBytes));

            return httpClient;
        }

        /// <summary>
        ///     Retrieves the access token to access the bot account
        /// </summary>
        public async Task<AccessTokenData> GetAccessToken()
        {
            using (HttpClient httpClient = this.GetHttpClient())
            {
                HttpResponseMessage response =
                    await httpClient.PostAsync(
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
                        await response.Content.ReadAsStringAsync(), RedditApiSettings.SerializerSettings);
                
                DateTime utcResponseDate =
                    response.Headers.Date?.UtcDateTime ?? DateTime.UtcNow;

                return new AccessTokenData
                {
                    AccessToken = content.AccessToken,
                    UtcExpirationDate = utcResponseDate.AddSeconds(content.ExpiresIn),
                };
            }
        }
    }
}
