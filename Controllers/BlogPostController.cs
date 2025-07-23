using abaBackOffice.Interfaces.Services;
using abaBackOffice.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        private readonly ILogger<BlogPostController> _logger;

        public BlogPostController(IBlogPostService blogPostService, ILogger<BlogPostController> logger)
        {
            _blogPostService = blogPostService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPostDto>>> GetAllBlogPosts()
        {
            _logger.LogInformation("Retrieving all blog posts");
            var posts = await _blogPostService.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetBlogPostById(int id)
        {
            _logger.LogInformation($"Retrieving blog post with id {id}");
            var post = await _blogPostService.GetByIdAsync(id);
            if (post == null)
            {
                _logger.LogWarning($"Blog post with id {id} not found");
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<BlogPostDto>> CreateBlogPost(BlogPostDto blogPostDto)
        {
            _logger.LogInformation("Creating a new blog post");
            var createdPost = await _blogPostService.CreateAsync(blogPostDto);
            return CreatedAtAction(nameof(GetBlogPostById), new { id = createdPost.Id }, createdPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, BlogPostDto blogPostDto)
        {
            if (id != blogPostDto.Id)
            {
                _logger.LogWarning("Blog post ID mismatch");
                return BadRequest();
            }

            _logger.LogInformation($"Updating blog post with id {id}");
            var updatedPost = await _blogPostService.UpdateAsync(blogPostDto);
            return Ok(updatedPost);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            _logger.LogInformation($"Deleting blog post with id {id}");
            await _blogPostService.DeleteAsync(id);
            return NoContent();
        }
    }
}
