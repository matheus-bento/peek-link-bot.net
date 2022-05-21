using PeekLinkBot.Reddit.Api;
using PeekLinkBot.Reddit.Auth;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PeekLinkBot
{
    public class PeekLinkBotService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<PeekLinkBotService> _logger;
        private readonly IOptions<PeekLinkBotConfig> _config;
        private readonly IHttpClientFactory _httpClientFactory;

        private RedditAuth _auth;

        public PeekLinkBotService(
            IHostApplicationLifetime hostApplicationLifetime,
            IHttpClientFactory httpClientFactory,
            IOptions<PeekLinkBotConfig> config,
            ILogger<PeekLinkBotService> logger)
        {
            this._hostApplicationLifetime = hostApplicationLifetime;
            this._httpClientFactory = httpClientFactory;
            this._config = config;
            this._logger = logger;

            this._auth =
                new RedditAuth(
                    this._httpClientFactory,
                    this._logger,
                    this._config.Value.Username,
                    this._config.Value.Password,
                    this._config.Value.ClientID,
                    this._config.Value.Secret
                );
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._logger.LogDebug("Service started");
            this._logger.LogDebug("Informed username: {0}", this._config.Value.Username);
            this._logger.LogDebug("Informed password: {0}", this._config.Value.Password);
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
                string accessToken = this._auth.GetAccessToken().Result.AccessToken;

                new RedditAPI(
                    this._httpClientFactory,
                    this._logger,
                    accessToken).GetUnreadMessages().Wait();
            }
            catch (HttpRequestException httpRequestException)
            {
                this._logger.LogError(httpRequestException, "An error occurred during the request to the reddit API");
            }
        }
    }
}
