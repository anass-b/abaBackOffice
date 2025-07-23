// -------------------- BlogPostRepository --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<BlogPostRepository> _logger;

        public BlogPostRepository(AbaDbContext context, ILogger<BlogPostRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all blog posts from database");
                return await _context.BlogPosts
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blog posts from database");
                throw;
            }
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving blog post with id {id} from database");
                return await _context.BlogPosts.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving blog post with id {id} from database");
                throw;
            }
        }

        public async Task CreateAsync(BlogPost blogPost)
        {
            try
            {
                _logger.LogInformation("Creating new blog post in database");
                await _context.BlogPosts.AddAsync(blogPost);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating blog post in database");
                throw;
            }
        }

        public async Task UpdateAsync(BlogPost blogPost)
        {
            try
            {
                _logger.LogInformation($"Updating blog post with id {blogPost.Id} in database");
                _context.BlogPosts.Update(blogPost);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating blog post with id {blogPost.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting blog post with id {id} from database");
                var post = await _context.BlogPosts.FindAsync(id);
                if (post != null)
                {
                    _context.BlogPosts.Remove(post);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting blog post with id {id} from database");
                throw;
            }
        }

    }
}
