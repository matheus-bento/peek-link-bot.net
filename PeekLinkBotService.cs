using PeekLinkBot.Reddit;
using PeekLinkBot.Auth;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PeekLinkBot.Reddit.Model;
using Newtonsoft.Json;

namespace PeekLinkBot
{
    public class PeekLinkBotService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger _logger;
        private readonly IOptions<PeekLinkBotConfig> _config;
        private readonly HttpClient _httpClient;

        private RedditAuth _auth;

        public PeekLinkBotService(
            IHostApplicationLifetime hostApplicationLifetime,
            ILogger<PeekLinkBotService> logger,
            IOptions<PeekLinkBotConfig> config,
            HttpClient httpClient)
        {
            this._hostApplicationLifetime = hostApplicationLifetime;
            this._logger = logger;
            this._config = config;
            this._httpClient = httpClient;

            this._auth =
                new RedditAuth(
                    this._httpClient,
                    this._config.Value.Username,
                    this._config.Value.Password,
                    this._config.Value.ClientID,
                    this._config.Value.Secret
                );
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._logger.LogDebug("Service started");
            this._logger.LogDebug("Informed username: " + this._config.Value.Username);
            this._logger.LogDebug("Informed password: " + this._config.Value.Password);
            this._logger.LogDebug("Service started");

            this._hostApplicationLifetime.ApplicationStarted.Register(this.OnServiceStarted);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._logger.LogDebug("Service stopped");

            return Task.CompletedTask;
        }

        private void OnServiceStarted()
        {
            try
            {
                RedditUserIdentity acctInfo = new RedditAPI(
                    this._httpClient,
                    this._auth.GetAccessToken().AccessToken
                ).GetAccountInfo();

                this._logger.LogDebug("Account Info: " + JsonConvert.SerializeObject(acctInfo, Formatting.Indented));
            }
            catch (HttpRequestException httpRequestException)
            {
                this._logger.LogError(httpRequestException, "An error occurred during the request to the reddit API");
            }
        }
    }
}
