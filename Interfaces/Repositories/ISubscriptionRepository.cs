using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAllAsync();
        Task<Subscription?> GetByIdAsync(int id);
        Task CreateAsync(Subscription entity);
        Task UpdateAsync(Subscription entity);
        Task DeleteAsync(int id);
    }
}
