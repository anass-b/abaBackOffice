using abaBackOffice.Interfaces.Services;
using abaBackOffice.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbllsTaskController : ControllerBase
    {
        private readonly IAbllsTaskService _abllsTaskService;
        private readonly ILogger<AbllsTaskController> _logger;

        public AbllsTaskController(IAbllsTaskService abllsTaskService, ILogger<AbllsTaskController> logger)
        {
            _abllsTaskService = abllsTaskService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbllsTaskDto>>> GetAllTasks()
        {
            _logger.LogInformation("Retrieving all ABLLS tasks");
            var tasks = await _abllsTaskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AbllsTaskDto>> GetTaskById(int id)
        {
            _logger.LogInformation($"Retrieving ABLLS task with id {id}");
            var task = await _abllsTaskService.GetByIdAsync(id);
            if (task == null)
            {
                _logger.LogWarning($"ABLLS task with id {id} not found");
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<AbllsTaskDto>> CreateTask(AbllsTaskDto abllsTaskDto)
        {
            _logger.LogInformation("Creating a new ABLLS task");
            var createdTask = await _abllsTaskService.CreateAsync(abllsTaskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, AbllsTaskDto abllsTaskDto)
        {
            if (id != abllsTaskDto.Id)
            {
                _logger.LogWarning("ABLLS task ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating ABLLS task with id {id}");
            var updatedTask = await _abllsTaskService.UpdateAsync(abllsTaskDto);
            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            _logger.LogInformation($"Deleting ABLLS task with id {id}");
            await _abllsTaskService.DeleteAsync(id);
            return NoContent();
        }
    }
}
