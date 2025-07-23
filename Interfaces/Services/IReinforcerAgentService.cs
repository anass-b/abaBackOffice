using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IReinforcerAgentService
    {
        Task<IEnumerable<ReinforcerAgentDto>> GetAllAsync();
        Task<ReinforcerAgentDto> GetByIdAsync(int id);
        Task<ReinforcerAgentDto> CreateAsync(ReinforcerAgentDto dto);
        Task<ReinforcerAgentDto> UpdateAsync(ReinforcerAgentDto dto);
        Task DeleteAsync(int id);
    }
}