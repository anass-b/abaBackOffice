using abaBackOffice.Interfaces.Services;
using abaBackOffice.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReinforcementProgramController : ControllerBase
    {
        private readonly IReinforcementProgramService _reinforcementProgramService;
        private readonly ILogger<ReinforcementProgramController> _logger;

        public ReinforcementProgramController(IReinforcementProgramService reinforcementProgramService, ILogger<ReinforcementProgramController> logger)
        {
            _reinforcementProgramService = reinforcementProgramService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReinforcementProgramDto>>> GetAllPrograms()
        {
            _logger.LogInformation("Retrieving all reinforcement programs");
            var programs = await _reinforcementProgramService.GetAllAsync();
            return Ok(programs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReinforcementProgramDto>> GetProgramById(int id)
        {
            _logger.LogInformation($"Retrieving reinforcement program with id {id}");
            var program = await _reinforcementProgramService.GetByIdAsync(id);
            if (program == null)
            {
                _logger.LogWarning($"Reinforcement program with id {id} not found");
                return NotFound();
            }
            return Ok(program);
        }

        [HttpPost]
        public async Task<ActionResult<ReinforcementProgramDto>> CreateProgram(ReinforcementProgramDto programDto)
        {
            _logger.LogInformation("Creating a new reinforcement program");
            var createdProgram = await _reinforcementProgramService.CreateAsync(programDto);
            return CreatedAtAction(nameof(GetProgramById), new { id = createdProgram.Id }, createdProgram);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgram(int id, ReinforcementProgramDto programDto)
        {
            if (id != programDto.Id)
            {
                _logger.LogWarning("Reinforcement program ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating reinforcement program with id {id}");
            var updatedProgram = await _reinforcementProgramService.UpdateAsync(programDto);
            return Ok(updatedProgram);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            _logger.LogInformation($"Deleting reinforcement program with id {id}");
            await _reinforcementProgramService.DeleteAsync(id);
            return NoContent();
        }
    }
}
