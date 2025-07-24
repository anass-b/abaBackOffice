using abaBackOffice.DTOs;
using abaBackOffice.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            _logger.LogInformation("Retrieving all categories");
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            _logger.LogInformation($"Retrieving category with id {id}");
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                _logger.LogWarning($"Category with id {id} not found");
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            _logger.LogInformation("Creating new category");
            var created = await _categoryService.CreateAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                _logger.LogWarning("Category ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating category with id {id}");
            var updated = await _categoryService.UpdateAsync(categoryDto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            _logger.LogInformation($"Deleting category with id {id}");
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
