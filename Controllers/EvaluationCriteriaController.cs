using abaBackOffice.DTOs;
using abaBackOffice.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationCriteriaController : ControllerBase
    {
        private readonly IEvaluationCriteriaService _service;
        private readonly ILogger<EvaluationCriteriaController> _logger;

        public EvaluationCriteriaController(IEvaluationCriteriaService service, ILogger<EvaluationCriteriaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluationCriteriaDto>>> GetAll()
        {
            _logger.LogInformation("Retrieving all EvaluationCriterias");
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationCriteriaDto>> GetById(int id)
        {
            _logger.LogInformation($"Retrieving EvaluationCriteria with id {id}");
            try
            {
                var item = await _service.GetByIdAsync(id);
                return Ok(item);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"EvaluationCriteria with id {id} not found");
                return NotFound();
            }
        }

        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<IEnumerable<EvaluationCriteriaDto>>> GetByTaskId(int taskId)
        {
            _logger.LogInformation($"Retrieving EvaluationCriterias for task id {taskId}");
            var items = await _service.GetByTaskIdAsync(taskId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<EvaluationCriteriaDto>> Create([FromBody] EvaluationCriteriaDto dto)
        {
            _logger.LogInformation("Creating EvaluationCriteria");
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EvaluationCriteriaDto dto)
        {
            if (id != dto.Id)
            {
                _logger.LogWarning("EvaluationCriteria ID mismatch");
                return BadRequest("ID mismatch");
            }

            _logger.LogInformation($"Updating EvaluationCriteria with id {id}");
            var updated = await _service.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting EvaluationCriteria with id {id}");
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
