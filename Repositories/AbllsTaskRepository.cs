// -------------------- AbllsTaskRepository --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class AbllsTaskRepository : IAbllsTaskRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<AbllsTaskRepository> _logger;

        public AbllsTaskRepository(AbaDbContext context, ILogger<AbllsTaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<AbllsTask>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all ABLLS tasks from database");
                return await _context.AbllsTasks
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ABLLS tasks from database");
                throw;
            }
        }

        public async Task<AbllsTask> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving ABLLS task with id {id} from database");
                return await _context.AbllsTasks.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving ABLLS task with id {id} from database");
                throw;
            }
        }

        public async Task CreateAsync(AbllsTask abllsTask)
        {
            try
            {
                _logger.LogInformation("Creating new ABLLS task in database");
                await _context.AbllsTasks.AddAsync(abllsTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ABLLS task in database");
                throw;
            }
        }

        public async Task UpdateAsync(AbllsTask abllsTask)
        {
            try
            {
                _logger.LogInformation($"Updating ABLLS task with id {abllsTask.Id} in database");
                _context.AbllsTasks.Update(abllsTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating ABLLS task with id {abllsTask.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting ABLLS task with id {id} from database");
                var task = await _context.AbllsTasks.FindAsync(id);
                if (task != null)
                {
                    _context.AbllsTasks.Remove(task);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting ABLLS task with id {id} from database");
                throw;
            }
        }

    }
}
