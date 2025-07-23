using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(int id);
        Task CreateAsync(BlogPost entity);
        Task UpdateAsync(BlogPost entity);
        Task DeleteAsync(int id);
    }
}