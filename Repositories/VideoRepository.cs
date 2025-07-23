// -------------------- VideoRepository --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<VideoRepository> _logger;

        public VideoRepository(AbaDbContext context, ILogger<VideoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Video>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all videos from database");
                return await _context.Videos
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving videos from database");
                throw;
            }
        }

        public async Task<Video> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving video with id {id} from database");
                return await _context.Videos.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving video with id {id} from database");
                throw;
            }
        }

        public async Task CreateAsync(Video video)
        {
            try
            {
                _logger.LogInformation("Creating new video in database");
                await _context.Videos.AddAsync(video);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating video in database");
                throw;
            }
        }

        public async Task UpdateAsync(Video video)
        {
            try
            {
                _logger.LogInformation($"Updating video with id {video.Id} in database");
                _context.Videos.Update(video);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating video with id {video.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting video with id {id} from database");
                var video = await _context.Videos.FindAsync(id);
                if (video != null)
                {
                    _context.Videos.Remove(video);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting video with id {id} from database");
                throw;
            }
        }


    }
}
