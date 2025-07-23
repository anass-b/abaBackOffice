// -------------------- BlogCommentRepository --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<BlogCommentRepository> _logger;

        public BlogCommentRepository(AbaDbContext context, ILogger<BlogCommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<BlogComment>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all blog comments from database");
                return await _context.BlogComments
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blog comments from database");
                throw;
            }
        }

        public async Task<BlogComment> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving blog comment with id {id} from database");
                return await _context.BlogComments.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving blog comment with id {id} from database");
                throw;
            }
        }

        public async Task CreateAsync(BlogComment blogComment)
        {
            try
            {
                _logger.LogInformation("Creating new blog comment in database");
                await _context.BlogComments.AddAsync(blogComment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating blog comment in database");
                throw;
            }
        }

        public async Task UpdateAsync(BlogComment blogComment)
        {
            try
            {
                _logger.LogInformation($"Updating blog comment with id {blogComment.Id} in database");
                _context.BlogComments.Update(blogComment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating blog comment with id {blogComment.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting blog comment with id {id} from database");
                var comment = await _context.BlogComments.FindAsync(id);
                if (comment != null)
                {
                    _context.BlogComments.Remove(comment);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting blog comment with id {id} from database");
                throw;
            }
        }

    }
}
