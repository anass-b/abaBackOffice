using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IAbllsTaskService
    {
        Task<IEnumerable<AbllsTaskDto>> GetAllAsync();
        Task<AbllsTaskDto> GetByIdAsync(int id);
        Task<AbllsTaskDto> CreateAsync(AbllsTaskDto dto);
        Task<AbllsTaskDto> UpdateAsync(AbllsTaskDto dto);
        Task DeleteAsync(int id);
    }
}