using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryDto dto);
        Task<CategoryDto> UpdateAsync(CategoryDto dto);
        Task DeleteAsync(int id);
    }
}
