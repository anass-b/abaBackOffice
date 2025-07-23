using abaBackOffice.Interfaces.Services;
using abaBackOffice.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReinforcerAgentController : ControllerBase
    {
        private readonly IReinforcerAgentService _reinforcerAgentService;
        private readonly ILogger<ReinforcerAgentController> _logger;

        public ReinforcerAgentController(IReinforcerAgentService reinforcerAgentService, ILogger<ReinforcerAgentController> logger)
        {
            _reinforcerAgentService = reinforcerAgentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReinforcerAgentDto>>> GetAllAgents()
        {
            _logger.LogInformation("Retrieving all reinforcer agents");
            var agents = await _reinforcerAgentService.GetAllAsync();
            return Ok(agents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReinforcerAgentDto>> GetAgentById(int id)
        {
            _logger.LogInformation($"Retrieving reinforcer agent with id {id}");
            var agent = await _reinforcerAgentService.GetByIdAsync(id);
            if (agent == null)
            {
                _logger.LogWarning($"Reinforcer agent with id {id} not found");
                return NotFound();
            }
            return Ok(agent);
        }

        [HttpPost]
        public async Task<ActionResult<ReinforcerAgentDto>> CreateAgent(ReinforcerAgentDto agentDto)
        {
            _logger.LogInformation("Creating a new reinforcer agent");
            var createdAgent = await _reinforcerAgentService.CreateAsync(agentDto);
            return CreatedAtAction(nameof(GetAgentById), new { id = createdAgent.Id }, createdAgent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgent(int id, ReinforcerAgentDto agentDto)
        {
            if (id != agentDto.Id)
            {
                _logger.LogWarning("Reinforcer agent ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating reinforcer agent with id {id}");
            var updatedAgent = await _reinforcerAgentService.UpdateAsync(agentDto);
            return Ok(updatedAgent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            _logger.LogInformation($"Deleting reinforcer agent with id {id}");
            await _reinforcerAgentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
