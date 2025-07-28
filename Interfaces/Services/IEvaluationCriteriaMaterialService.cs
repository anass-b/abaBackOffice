using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IEvaluationCriteriaMaterialService
    {
        Task<IEnumerable<EvaluationCriteriaMaterialDto>> GetAllAsync();
        Task<IEnumerable<EvaluationCriteriaMaterialDto>> GetByCriteriaIdAsync(int criteriaId);
        Task CreateAsync(EvaluationCriteriaMaterialDto dto);
        Task DeleteByCriteriaIdAsync(int criteriaId);
    }
}
