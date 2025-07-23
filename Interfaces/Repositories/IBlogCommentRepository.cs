using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IBlogCommentRepository
    {
        Task<IEnumerable<BlogComment>> GetAllAsync();
        Task<BlogComment?> GetByIdAsync(int id);
        Task CreateAsync(BlogComment entity);
        Task UpdateAsync(BlogComment entity);
        Task DeleteAsync(int id);
    }
}