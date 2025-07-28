using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class EvaluationCriteriaMaterialRepository : IEvaluationCriteriaMaterialRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<EvaluationCriteriaMaterialRepository> _logger;

        public EvaluationCriteriaMaterialRepository(AbaDbContext context, ILogger<EvaluationCriteriaMaterialRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<EvaluationCriteriaMaterial>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all EvaluationCriteriaMaterials");
                return await _context.EvaluationCriteriaMaterials
                    .Include(e => e.MaterialPhoto)
                    .Include(e => e.EvaluationCriteria)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving EvaluationCriteriaMaterials");
                throw;
            }
        }

        public async Task<IEnumerable<EvaluationCriteriaMaterial>> GetByCriteriaIdAsync(int criteriaId)
        {
            try
            {
                return await _context.EvaluationCriteriaMaterials
                    .Where(x => x.EvaluationCriteriaId == criteriaId)
                    .Include(x => x.MaterialPhoto)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving EvaluationCriteriaMaterials for criteriaId {criteriaId}");
                throw;
            }
        }

        public async Task CreateAsync(EvaluationCriteriaMaterial entity)
        {
            try
            {
                _logger.LogInformation("Creating EvaluationCriteriaMaterial");
                await _context.EvaluationCriteriaMaterials.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating EvaluationCriteriaMaterial");
                throw;
            }
        }

        public async Task DeleteByCriteriaIdAsync(int criteriaId)
        {
            try
            {
                var existing = await _context.EvaluationCriteriaMaterials
                    .Where(x => x.EvaluationCriteriaId == criteriaId)
                    .ToListAsync();

                _context.EvaluationCriteriaMaterials.RemoveRange(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting EvaluationCriteriaMaterials by criteriaId {criteriaId}");
                throw;
            }
        }
        public IQueryable<EvaluationCriteriaMaterial> GetQueryable()
        {
            return _context.EvaluationCriteriaMaterials.AsQueryable();
        }

    }
}
