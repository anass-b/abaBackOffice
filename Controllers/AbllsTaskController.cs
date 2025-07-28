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
            try
            {
                var task = await _abllsTaskService.GetByIdAsync(id);
                return Ok(task);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"ABLLS task with id {id} not found");
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<AbllsTaskDto>> CreateTask([FromForm] AbllsTaskDto abllsTaskDto)
        {
            _logger.LogInformation("Creating a new ABLLS task");
            var createdTask = await _abllsTaskService.CreateAsync(abllsTaskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromForm] AbllsTaskDto abllsTaskDto)
        {
            if (id != abllsTaskDto.Id)
            {
                _logger.LogWarning("ABLLS task ID mismatch");
                return BadRequest("Task ID mismatch");
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
