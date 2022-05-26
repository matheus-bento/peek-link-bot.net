using PeekLinkBot.Reddit.Api;
using PeekLinkBot.Reddit.Auth;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PeekLinkBot.Reddit.Api.Model;
using PeekLinkBot.Reddit;
using System.Collections.Generic;
using System;
using System.Linq;
using PeekLinkBot.Scraper.Exceptions;

namespace PeekLinkBot
{
    public class PeekLinkBotService : IHostedService, IDisposable
    {
        private readonly ILogger<PeekLinkBotService> _logger;
        private readonly IOptions<PeekLinkBotConfig> _config;
        private readonly IHttpClientFactory _httpClientFactory;

        private Timer _messageCheckTimer;

        private RedditAuth _auth;

        public PeekLinkBotService(
            IHttpClientFactory httpClientFactory,
            IOptions<PeekLinkBotConfig> config,
            ILogger<PeekLinkBotService> logger)
        {
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

            this._messageCheckTimer =
                new Timer(
                    async (state) => await this.OnMessageCheckTimer(),
                    null,
                    TimeSpan.Zero,
                    TimeSpan.FromSeconds(this._config.Value.MessageCheckInterval));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._logger.LogDebug("Service stopped");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this._messageCheckTimer.Dispose();
        }

        private async Task OnMessageCheckTimer()
        {
            try
            {
                string accessToken = this._auth.GetAccessToken().Result.AccessToken;
                var redditApi = new RedditAPI(this._httpClientFactory, this._logger, accessToken);

                foreach (Message message in await redditApi.GetUnreadMentions())
                {
                    // Getting the comment/post targeted by the username mention that calls the bot
                    var targetMessage = await redditApi.GetMessageById(message.ParentId);

                    if (targetMessage != null)
                    {
                        var commentHandler = new CommentHandler(targetMessage.Body);

                        IEnumerable<string> linksInfo = await commentHandler.GetUrlInfo();

                        if (linksInfo.Count() > 0)
                        {
                            string reply = String.Format(
                                "{0}\n" +
                                "^(beep bop I'm /u/peek-link-bot, your friendly bot " +
                                "that checks links beforehand so you don't get bamboozled. " +
                                "Help me to improve by contributing on) " +
                                "^[github](https://github.com/matheus-bento/peek-link-bot-v2) ^:)",
                                String.Join('\n', linksInfo));

                            await redditApi.PostComment(message.Name, reply);
                        }
                    }

                    await redditApi.MarkMessageAsRead(message);
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                this._logger.LogError(httpRequestException, "An error occurred during the request to the reddit API");
            }
            catch (DocumentScrapingException scrapingException)
            {
                this._logger.LogError(scrapingException, "An error occured while scraping a document");
            }
        }
    }
}
