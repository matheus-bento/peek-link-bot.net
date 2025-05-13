using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PeekLinkBot.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InteractionsController : ControllerBase
    {
        private readonly ILogger<InteractionsController> _logger;

        public InteractionsController(ILogger<InteractionsController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Test()
        {
            this._logger.LogInformation("Test endpoint called");
            return Ok(new { ok = true });
        }
    }
}
