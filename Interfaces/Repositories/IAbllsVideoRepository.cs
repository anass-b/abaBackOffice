using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IAbllsVideoRepository
    {
        Task<IEnumerable<AbllsVideo>> GetAllAsync();
        Task<AbllsVideo?> GetByIdAsync(int id);
        Task CreateAsync(AbllsVideo entity);
        Task UpdateAsync(AbllsVideo entity);
        Task DeleteAsync(int id);
    }
}