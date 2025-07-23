using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task CreateAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(int id);
    }
}