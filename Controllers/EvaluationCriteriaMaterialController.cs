using abaBackOffice.Interfaces.Services;
using abaBackOffice.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationCriteriaMaterialController : ControllerBase
    {
        private readonly IEvaluationCriteriaMaterialService _service;
        private readonly ILogger<EvaluationCriteriaMaterialController> _logger;

        public EvaluationCriteriaMaterialController(IEvaluationCriteriaMaterialService service, ILogger<EvaluationCriteriaMaterialController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluationCriteriaMaterialDto>>> GetAll()
        {
            _logger.LogInformation("Retrieving all EvaluationCriteriaMaterial links");
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("criteria/{criteriaId}")]
        public async Task<ActionResult<IEnumerable<EvaluationCriteriaMaterialDto>>> GetByCriteriaId(int criteriaId)
        {
            _logger.LogInformation($"Retrieving materials for criteria ID {criteriaId}");
            var list = await _service.GetByCriteriaIdAsync(criteriaId);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EvaluationCriteriaMaterialDto dto)
        {
            _logger.LogInformation("Creating new EvaluationCriteriaMaterial link");
            await _service.CreateAsync(dto);
            return Ok(dto);
        }

        [HttpDelete("criteria/{criteriaId}")]
        public async Task<IActionResult> DeleteByCriteriaId(int criteriaId)
        {
            _logger.LogInformation($"Deleting EvaluationCriteriaMaterial links for criteria ID {criteriaId}");
            await _service.DeleteByCriteriaIdAsync(criteriaId);
            return NoContent();
        }
    }
}
