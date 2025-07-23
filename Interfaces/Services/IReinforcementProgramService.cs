using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IReinforcementProgramService
    {
        Task<IEnumerable<ReinforcementProgramDto>> GetAllAsync();
        Task<ReinforcementProgramDto> GetByIdAsync(int id);
        Task<ReinforcementProgramDto> CreateAsync(ReinforcementProgramDto dto);
        Task<ReinforcementProgramDto> UpdateAsync(ReinforcementProgramDto dto);
        Task DeleteAsync(int id);
    }
}
