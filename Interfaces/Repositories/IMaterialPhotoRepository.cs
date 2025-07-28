using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IMaterialPhotoRepository
    {
        Task<IEnumerable<MaterialPhoto>> GetAllAsync();
        Task<MaterialPhoto?> GetByIdAsync(int id);
        Task<IEnumerable<MaterialPhoto>> GetByTaskIdAsync(int taskId);
        Task CreateAsync(MaterialPhoto entity);
        Task UpdateAsync(MaterialPhoto entity);
        Task DeleteAsync(int id);
    }
}
