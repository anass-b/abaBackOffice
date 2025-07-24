using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(AbaDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all categories from database");
                return await _context.Categories.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving categories");
                throw;
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving category with id {id}");
                return await _context.Categories.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving category with id {id}");
                throw;
            }
        }

        public async Task CreateAsync(Category category)
        {
            try
            {
                _logger.LogInformation("Creating category");
                await _context.Categories.AddAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                throw;
            }
        }

        public async Task UpdateAsync(Category category)
        {
            try
            {
                _logger.LogInformation($"Updating category with id {category.Id}");
                _context.Categories.Update(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating category with id {category.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting category with id {id}");
                var category = await _context.Categories.FindAsync(id);
                if (category != null)
                    _context.Categories.Remove(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting category with id {id}");
                throw;
            }
        }
    }
}
