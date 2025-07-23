using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAllAsync();
        Task<Document?> GetByIdAsync(int id);
        Task CreateAsync(Document entity);
        Task UpdateAsync(Document entity);
        Task DeleteAsync(int id);
    }
}