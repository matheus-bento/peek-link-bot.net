using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace PeekLinkBot
{
    public class PeekLinkBotService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IOptions<PeekLinkBotConfig> _config;

        public PeekLinkBotService(ILogger<PeekLinkBotService> logger, IOptions<PeekLinkBotConfig> config)
        {
            this._logger = logger;
            this._config = config;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._logger.LogDebug("Service started");
            this._logger.LogDebug("Informed username: " + this._config.Value.Username);
            this._logger.LogDebug("Informed password: " + this._config.Value.Password);
            this._logger.LogDebug("Service started");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._logger.LogDebug("Service stopped");
            
            return Task.CompletedTask;
        }
    }
}
