using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IAbllsTaskRepository
    {
        Task<IEnumerable<AbllsTask>> GetAllAsync();
        Task<AbllsTask?> GetByIdAsync(int id);
        Task CreateAsync(AbllsTask entity);
        Task UpdateAsync(AbllsTask entity);
        Task DeleteAsync(int id);
    }
}