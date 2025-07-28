using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class BaselineContentRepository : IBaselineContentRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<BaselineContentRepository> _logger;

        public BaselineContentRepository(AbaDbContext context, ILogger<BaselineContentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<BaselineContent>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all BaselineContents from database");
                return await _context.BaselineContents
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving BaselineContents from database");
                throw;
            }
        }

        public async Task<BaselineContent?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving BaselineContent with id {id} from database");
                return await _context.BaselineContents.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving BaselineContent with id {id} from database");
                throw;
            }
        }

        public async Task<IEnumerable<BaselineContent>> GetByTaskIdAsync(int taskId)
        {
            try
            {
                _logger.LogInformation($"Retrieving BaselineContents for task {taskId}");
                return await _context.BaselineContents
                    .Where(b => b.AbllsTaskId == taskId)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving BaselineContents for task {taskId}");
                throw;
            }
        }

        public async Task CreateAsync(BaselineContent entity)
        {
            try
            {
                _logger.LogInformation("Creating BaselineContent in database");
                await _context.BaselineContents.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating BaselineContent in database");
                throw;
            }
        }

        public async Task UpdateAsync(BaselineContent entity)
        {
            try
            {
                _logger.LogInformation($"Updating BaselineContent with id {entity.Id} in database");
                _context.BaselineContents.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating BaselineContent with id {entity.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting BaselineContent with id {id} from database");
                var existing = await _context.BaselineContents.FindAsync(id);
                if (existing != null)
                    _context.BaselineContents.Remove(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting BaselineContent with id {id} from database");
                throw;
            }
        }
    }
}
