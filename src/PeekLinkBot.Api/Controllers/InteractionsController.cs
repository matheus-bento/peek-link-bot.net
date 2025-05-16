using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeekLinkBot.Data.Repositories;
using PeekLinkBot.Domain.Dto;
using PeekLinkBot.Domain.UseCases;

namespace PeekLinkBot.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class InteractionsController : ControllerBase
    {
        private readonly ILogger<InteractionsController> _logger;
        private readonly IBotInteractionRepository _botInteractionRepository;

        public InteractionsController(
            ILogger<InteractionsController> logger,
            IBotInteractionRepository botInteractionRepository)
        {
            this._logger = logger;
            this._botInteractionRepository = botInteractionRepository;
        }

        // TODO: Add paging later
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var useCase =
                    new GetBotInteractionsUseCase(this._botInteractionRepository);

                IEnumerable<BotInteractionDto> interactions = await useCase.Execute();

                return Ok(interactions);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "An error occurred while retrieving bot interactions");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("{redditId}")]
        public async Task<IActionResult> GetByRedditId(string redditId)
        {
            try
            {
                var useCase =
                    new GetBotInteractionByRedditIdUseCase(
                        this._botInteractionRepository);
                
                BotInteractionDto interaction =
                    await useCase.Execute(redditId);

                if (interaction == null)
                    return NotFound("Interaction not found");

                return Ok(interaction);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "An error occurred while retrieving bot interactions");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
