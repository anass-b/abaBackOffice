using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IEvaluationCriteriaRepository
    {
        Task<IEnumerable<EvaluationCriteria>> GetAllAsync();
        Task<EvaluationCriteria?> GetByIdAsync(int id);
        Task<IEnumerable<EvaluationCriteria>> GetByTaskIdAsync(int taskId);
        Task<EvaluationCriteria> CreateAsync(EvaluationCriteria entity);
        Task UpdateAsync(EvaluationCriteria entity);
        Task DeleteAsync(int id);
    }
}
