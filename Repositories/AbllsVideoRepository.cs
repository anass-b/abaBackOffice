// -------------------- AbllsVideoRepository --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class AbllsVideoRepository : IAbllsVideoRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<AbllsVideoRepository> _logger;

        public AbllsVideoRepository(AbaDbContext context, ILogger<AbllsVideoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<AbllsVideo>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all ABLLS videos from database");
                return await _context.AbllsVideos
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ABLLS videos from database");
                throw;
            }
        }

        public async Task<AbllsVideo> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving ABLLS video with id {id} from database");
                return await _context.AbllsVideos.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving ABLLS video with id {id} from database");
                throw;
            }
        }

        public async Task CreateAsync(AbllsVideo abllsVideo)
        {
            try
            {
                _logger.LogInformation("Creating new ABLLS video in database");
                await _context.AbllsVideos.AddAsync(abllsVideo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ABLLS video in database");
                throw;
            }
        }

        public async Task UpdateAsync(AbllsVideo abllsVideo)
        {
            try
            {
                _logger.LogInformation($"Updating ABLLS video with id {abllsVideo.Id} in database");
                _context.AbllsVideos.Update(abllsVideo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating ABLLS video with id {abllsVideo.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting ABLLS video with id {id} from database");
                var video = await _context.AbllsVideos.FindAsync(id);
                if (video != null)
                {
                    _context.AbllsVideos.Remove(video);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting ABLLS video with id {id} from database");
                throw;
            }
        }

    }
}
