using PeekLinkBot.Reddit.Model;

using System;
using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PeekLinkBot.Reddit
{
    public class RedditAPI
    {
        private readonly Uri BASE_REDDIT_URI = new Uri("https://oauth.reddit.com/");

        private readonly HttpClient _httpClient;
        private readonly string _accessToken;

        public RedditAPI(HttpClient httpClient, string accessToken)
        {
            this._httpClient = httpClient;
            this._accessToken = accessToken;
        }

        public RedditUserIdentity GetAccountInfo()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.BASE_REDDIT_URI, "/api/v1/me"),
                Method = HttpMethod.Get
            };

            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("PeekLinkBot", "1.0"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this._accessToken);

            HttpResponseMessage response = this._httpClient.Send(request);

            RedditUserIdentity content = 
                JsonConvert.DeserializeObject<RedditUserIdentity>(
                    response.Content.ReadAsStringAsync().Result,
                    new JsonSerializerSettings 
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    });

            return content;
        }
    }
}
