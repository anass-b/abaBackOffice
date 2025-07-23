using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IReinforcementProgramRepository
    {
        Task<IEnumerable<ReinforcementProgram>> GetAllAsync();
        Task<ReinforcementProgram?> GetByIdAsync(int id);
        Task CreateAsync(ReinforcementProgram entity);
        Task UpdateAsync(ReinforcementProgram entity);
        Task DeleteAsync(int id);
    }
}
