using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class MaterialPhotoRepository : IMaterialPhotoRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<MaterialPhotoRepository> _logger;

        public MaterialPhotoRepository(AbaDbContext context, ILogger<MaterialPhotoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<MaterialPhoto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all MaterialPhotos from database");
                return await _context.MaterialPhotos
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving MaterialPhotos");
                throw;
            }
        }

        public async Task<MaterialPhoto?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving MaterialPhoto with id {id}");
                return await _context.MaterialPhotos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving MaterialPhoto with id {id}");
                throw;
            }
        }

        public async Task<IEnumerable<MaterialPhoto>> GetByTaskIdAsync(int taskId)
        {
            try
            {
                _logger.LogInformation($"Retrieving MaterialPhotos for taskId {taskId}");
                return await _context.MaterialPhotos
                    .Where(p => p.AbllsTaskId == taskId)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving MaterialPhotos for taskId {taskId}");
                throw;
            }
        }

        public async Task CreateAsync(MaterialPhoto entity)
        {
            try
            {
                _logger.LogInformation("Creating new MaterialPhoto");
                await _context.MaterialPhotos.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating MaterialPhoto");
                throw;
            }
        }

        public async Task UpdateAsync(MaterialPhoto entity)
        {
            try
            {
                _logger.LogInformation($"Updating MaterialPhoto with id {entity.Id}");
                _context.MaterialPhotos.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating MaterialPhoto with id {entity.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting MaterialPhoto with id {id}");
                var existing = await _context.MaterialPhotos.FindAsync(id);
                if (existing != null)
                    _context.MaterialPhotos.Remove(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting MaterialPhoto with id {id}");
                throw;
            }
        }
    }
}
