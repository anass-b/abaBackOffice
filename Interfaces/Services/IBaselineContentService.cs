using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IBaselineContentService
    {
        Task<IEnumerable<BaselineContentDto>> GetAllAsync();
        Task<BaselineContentDto> GetByIdAsync(int id);
        Task<IEnumerable<BaselineContentDto>> GetByTaskIdAsync(int taskId);
        Task<BaselineContentDto> CreateAsync(BaselineContentDto dto);
        Task<BaselineContentDto> UpdateAsync(BaselineContentDto dto);
        Task DeleteAsync(int id);
    }
}
