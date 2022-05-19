using PeekLinkBot.Auth.Model;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PeekLinkBot.Auth
{
    public class RedditAuth
    {
        private HttpClient _httpClient;

        private string _username;
        private string _password;
        
        private string _clientID;
        private string _secret;

        public RedditAuth(
            HttpClient httpClient,
            string username,
            string password,
            string clientID,
            string secret)
        {
            this._httpClient = httpClient;
            
            this._username = username;
            this._password = password;
            
            this._clientID = clientID;
            this._secret = secret;
        }

        public AccessTokenResponseContent GetAccessToken()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("https://www.reddit.com/api/v1/access_token"),
                Method = HttpMethod.Post,
                Content = 
                    new FormUrlEncodedContent(
                        new Dictionary<string, string>
                        {
                            { "grant_type", "password" },
                            { "username", this._username },
                            { "password", this._password }
                        }
                    ),
            };

            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("PeekLinkBot", "1.0"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", GetBase64Credentials());

            HttpResponseMessage response = this._httpClient.Send(request);

            response.EnsureSuccessStatusCode();

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

        // Reddit's authorization endpoint requires an Authorization header
        // with identification for the client that will access the API,
        // using the client ID as the username and the secret as the password
        // informed using basic authentication
        private string GetBase64Credentials()
        {
            byte[] credsBytes = Encoding.ASCII.GetBytes(this._clientID + ":" + this._secret);
            return Convert.ToBase64String(credsBytes);
        }
    }
}
