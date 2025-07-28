using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IMaterialPhotoService
    {
        Task<IEnumerable<MaterialPhotoDto>> GetAllAsync();
        Task<MaterialPhotoDto> GetByIdAsync(int id);
        Task<IEnumerable<MaterialPhotoDto>> GetByTaskIdAsync(int taskId);
        Task<MaterialPhotoDto> CreateAsync(MaterialPhotoDto dto);
        Task<MaterialPhotoDto> UpdateAsync(MaterialPhotoDto dto);
        Task DeleteAsync(int id);
    }
}
