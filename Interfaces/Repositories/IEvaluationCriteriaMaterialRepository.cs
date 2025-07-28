using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IEvaluationCriteriaMaterialRepository
    {
        Task<IEnumerable<EvaluationCriteriaMaterial>> GetAllAsync();
        Task<IEnumerable<EvaluationCriteriaMaterial>> GetByCriteriaIdAsync(int criteriaId);
        Task CreateAsync(EvaluationCriteriaMaterial entity);
        Task DeleteByCriteriaIdAsync(int criteriaId);
        IQueryable<EvaluationCriteriaMaterial> GetQueryable();
    }
}
