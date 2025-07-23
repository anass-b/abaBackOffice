using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IReinforcerAgentRepository
    {
        Task<IEnumerable<ReinforcerAgent>> GetAllAsync();
        Task<ReinforcerAgent?> GetByIdAsync(int id);
        Task CreateAsync(ReinforcerAgent entity);
        Task UpdateAsync(ReinforcerAgent entity);
        Task DeleteAsync(int id);
    }
}