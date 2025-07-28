using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IBaselineContentRepository
    {
        Task<IEnumerable<BaselineContent>> GetAllAsync();
        Task<BaselineContent?> GetByIdAsync(int id);
        Task<IEnumerable<BaselineContent>> GetByTaskIdAsync(int taskId);
        Task CreateAsync(BaselineContent entity);
        Task UpdateAsync(BaselineContent entity);
        Task DeleteAsync(int id);
    }
}
