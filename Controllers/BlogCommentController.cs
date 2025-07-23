using abaBackOffice.Interfaces.Services;
using abaBackOffice.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogCommentController : ControllerBase
    {
        private readonly IBlogCommentService _blogCommentService;
        private readonly ILogger<BlogCommentController> _logger;

        public BlogCommentController(IBlogCommentService blogCommentService, ILogger<BlogCommentController> logger)
        {
            _blogCommentService = blogCommentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogCommentDto>>> GetAllBlogComments()
        {
            _logger.LogInformation("Retrieving all blog comments");
            var comments = await _blogCommentService.GetAllAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogCommentDto>> GetBlogCommentById(int id)
        {
            _logger.LogInformation($"Retrieving blog comment with id {id}");
            var comment = await _blogCommentService.GetByIdAsync(id);
            if (comment == null)
            {
                _logger.LogWarning($"Blog comment with id {id} not found");
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult<BlogCommentDto>> CreateBlogComment(BlogCommentDto blogCommentDto)
        {
            _logger.LogInformation("Creating a new blog comment");
            var createdComment = await _blogCommentService.CreateAsync(blogCommentDto);
            return CreatedAtAction(nameof(GetBlogCommentById), new { id = createdComment.Id }, createdComment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogComment(int id, BlogCommentDto blogCommentDto)
        {
            if (id != blogCommentDto.Id)
            {
                _logger.LogWarning("Blog comment ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating blog comment with id {id}");
            var updatedComment = await _blogCommentService.UpdateAsync(blogCommentDto);
            return Ok(updatedComment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogComment(int id)
        {
            _logger.LogInformation($"Deleting blog comment with id {id}");
            await _blogCommentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
