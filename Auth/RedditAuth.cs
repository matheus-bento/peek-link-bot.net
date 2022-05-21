using PeekLinkBot.Auth.Model;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PeekLinkBot.Auth
{
    public class RedditAuth
    {
        private readonly ILogger<PeekLinkBotService> _logger;
        private readonly HttpClient _redditAuthHttpClient;

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
            this._logger = logger;

            this._redditAuthHttpClient = httpClientFactory.CreateClient("RedditAuth");
            
            this._username = username;
            this._password = password;
            
            this._clientID = clientID;
            this._secret = secret;
        }

        // Reddit's authorization endpoint requires an Authorization header
        // with identification for the client that will access the API,
        // using the client ID as the username and the secret as the password
        // informed using basic authentication
        private string GetBase64Credentials()
        {
            byte[] credsBytes = Encoding.ASCII.GetBytes(this._clientID + ":" + this._secret);
            return Convert.ToBase64String(credsBytes);
        }

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

            AccessTokenResponseContent content = 
                JsonConvert.DeserializeObject<AccessTokenResponseContent>(
                    response.Content.ReadAsStringAsync().Result,
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
