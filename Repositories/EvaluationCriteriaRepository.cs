using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class EvaluationCriteriaRepository : IEvaluationCriteriaRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<EvaluationCriteriaRepository> _logger;

        public EvaluationCriteriaRepository(AbaDbContext context, ILogger<EvaluationCriteriaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<EvaluationCriteria>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all EvaluationCriterias");
                return await _context.EvaluationCriterias
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all EvaluationCriterias");
                throw;
            }
        }

        public async Task<EvaluationCriteria?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving EvaluationCriteria with id {id}");
                return await _context.EvaluationCriterias
                .Include(e => e.EvaluationCriteriaMaterials)
                .ThenInclude(link => link.MaterialPhoto)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving EvaluationCriteria with id {id}");
                throw;
            }
        }

        public async Task<IEnumerable<EvaluationCriteria>> GetByTaskIdAsync(int taskId)
        {
            try
            {
                _logger.LogInformation($"Retrieving EvaluationCriterias for taskId {taskId}");
                return await _context.EvaluationCriterias
                    .Where(e => e.AbllsTaskId == taskId)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving EvaluationCriterias for taskId {taskId}");
                throw;
            }
        }

        public async Task<EvaluationCriteria> CreateAsync(EvaluationCriteria entity)
        {
            try
            {
                _logger.LogInformation("Creating EvaluationCriteria");
                await _context.EvaluationCriterias.AddAsync(entity);
                await _context.SaveChangesAsync(); // ⚠️ obligatoire pour générer l'ID
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating EvaluationCriteria");
                throw;
            }
        }


        public async Task UpdateAsync(EvaluationCriteria entity)
        {
            try
            {
                _logger.LogInformation($"Updating EvaluationCriteria with id {entity.Id}");
                _context.EvaluationCriterias.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating EvaluationCriteria with id {entity.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting EvaluationCriteria with id {id}");
                var entity = await _context.EvaluationCriterias.FindAsync(id);
                if (entity != null)
                    _context.EvaluationCriterias.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting EvaluationCriteria with id {id}");
                throw;
            }
        }
    }
}
