using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IAbllsVideoService
    {
        Task<IEnumerable<AbllsVideoDto>> GetAllAsync();
        Task<AbllsVideoDto> GetByIdAsync(int id);
        Task<AbllsVideoDto> CreateAsync(AbllsVideoDto dto);
        Task<AbllsVideoDto> UpdateAsync(AbllsVideoDto dto);
        Task DeleteAsync(int id);
    }
}