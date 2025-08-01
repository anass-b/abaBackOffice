using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IDomainRepository
    {
        Task<IEnumerable<Domain>> GetAllAsync();
        Task<Domain?> GetByIdAsync(int id);
        Task<IEnumerable<Domain>> GetByCategoryIdAsync(int categoryId);
        Task CreateAsync(Domain domain);
        Task UpdateAsync(Domain domain);
        Task DeleteAsync(int id);
        IQueryable<Domain> GetQueryable(); // utile pour filtrages avancés
    }
}
