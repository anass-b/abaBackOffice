using abaBackOffice.DTOs;
using abaBackOffice.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DomainController : ControllerBase
    {
        private readonly IDomainService _domainService;
        private readonly ILogger<DomainController> _logger;

        public DomainController(IDomainService domainService, ILogger<DomainController> logger)
        {
            _domainService = domainService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DomainDto>>> GetAll()
        {
            _logger.LogInformation("Retrieving all domains");
            var domains = await _domainService.GetAllAsync();
            return Ok(domains);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DomainDto>> GetById(int id)
        {
            _logger.LogInformation($"Retrieving domain with id {id}");
            var domain = await _domainService.GetByIdAsync(id);
            if (domain == null)
            {
                _logger.LogWarning($"Domain with id {id} not found");
                return NotFound();
            }
            return Ok(domain);
        }

        [HttpGet("byCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<DomainDto>>> GetByCategory(int categoryId)
        {
            _logger.LogInformation($"Retrieving domains for category {categoryId}");
            var domains = await _domainService.GetByCategoryIdAsync(categoryId);
            return Ok(domains);
        }

        [HttpPost]
        public async Task<ActionResult<DomainDto>> Create([FromBody] DomainDto dto)
        {
            _logger.LogInformation("Creating new domain");
            var created = await _domainService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DomainDto dto)
        {
            if (id != dto.Id)
            {
                _logger.LogWarning("Domain ID mismatch");
                return BadRequest("ID mismatch");
            }

            _logger.LogInformation($"Updating domain with id {id}");
            var updated = await _domainService.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting domain with id {id}");
            await _domainService.DeleteAsync(id);
            return NoContent();
        }
    }
}
