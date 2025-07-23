// -------------------- ReinforcementProgramRepository --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class ReinforcementProgramRepository : IReinforcementProgramRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<ReinforcementProgramRepository> _logger;

        public ReinforcementProgramRepository(AbaDbContext context, ILogger<ReinforcementProgramRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ReinforcementProgram>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all reinforcement programs from database");
                return await _context.ReinforcementPrograms
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reinforcement programs from database");
                throw;
            }
        }

        public async Task<ReinforcementProgram> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving reinforcement program with id {id} from database");
                return await _context.ReinforcementPrograms.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving reinforcement program with id {id} from database");
                throw;
            }
        }

        public async Task CreateAsync(ReinforcementProgram program)
        {
            try
            {
                _logger.LogInformation("Creating new reinforcement program in database");
                await _context.ReinforcementPrograms.AddAsync(program);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating reinforcement program in database");
                throw;
            }
        }

        public async Task UpdateAsync(ReinforcementProgram program)
        {
            try
            {
                _logger.LogInformation($"Updating reinforcement program with id {program.Id} in database");
                _context.ReinforcementPrograms.Update(program);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating reinforcement program with id {program.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting reinforcement program with id {id} from database");
                var program = await _context.ReinforcementPrograms.FindAsync(id);
                if (program != null)
                {
                    _context.ReinforcementPrograms.Remove(program);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting reinforcement program with id {id} from database");
                throw;
            }
        }

    }
}
