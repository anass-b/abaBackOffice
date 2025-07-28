using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IEvaluationCriteriaService
    {
        Task<IEnumerable<EvaluationCriteriaDto>> GetAllAsync();
        Task<EvaluationCriteriaDto> GetByIdAsync(int id);
        Task<IEnumerable<EvaluationCriteriaDto>> GetByTaskIdAsync(int taskId);
        Task<EvaluationCriteriaDto> CreateAsync(EvaluationCriteriaDto dto);
        Task<EvaluationCriteriaDto> UpdateAsync(EvaluationCriteriaDto dto);
        Task DeleteAsync(int id);
    }
}
