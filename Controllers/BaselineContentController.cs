using abaBackOffice.DTOs;
using abaBackOffice.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaselineContentController : ControllerBase
    {
        private readonly IBaselineContentService _service;
        private readonly ILogger<BaselineContentController> _logger;

        public BaselineContentController(IBaselineContentService service, ILogger<BaselineContentController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaselineContentDto>>> GetAll()
        {
            _logger.LogInformation("Retrieving all BaselineContents");
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaselineContentDto>> GetById(int id)
        {
            _logger.LogInformation($"Retrieving BaselineContent with id {id}");
            try
            {
                var item = await _service.GetByIdAsync(id);
                return Ok(item);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"BaselineContent with id {id} not found");
                return NotFound();
            }
        }

        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<IEnumerable<BaselineContentDto>>> GetByTaskId(int taskId)
        {
            _logger.LogInformation($"Retrieving BaselineContents for task id {taskId}");
            var items = await _service.GetByTaskIdAsync(taskId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<BaselineContentDto>> Create(BaselineContentDto dto)
        {
            _logger.LogInformation("Creating BaselineContent");
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BaselineContentDto dto)
        {
            if (id != dto.Id)
            {
                _logger.LogWarning("BaselineContent ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating BaselineContent with id {id}");
            var updated = await _service.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting BaselineContent with id {id}");
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
