using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAllAsync();
        Task<Video?> GetByIdAsync(int id);
        Task CreateAsync(Video entity);
        Task UpdateAsync(Video entity);
        Task DeleteAsync(int id);
    }
}