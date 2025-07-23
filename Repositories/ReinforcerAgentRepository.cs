// -------------------- ReinforcerAgentRepository --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class ReinforcerAgentRepository : IReinforcerAgentRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<ReinforcerAgentRepository> _logger;

        public ReinforcerAgentRepository(AbaDbContext context, ILogger<ReinforcerAgentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ReinforcerAgent>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all reinforcer agents from database");
                return await _context.ReinforcerAgents
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reinforcer agents from database");
                throw;
            }
        }

        public async Task<ReinforcerAgent> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving reinforcer agent with id {id} from database");
                return await _context.ReinforcerAgents.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving reinforcer agent with id {id} from database");
                throw;
            }
        }

        public async Task CreateAsync(ReinforcerAgent agent)
        {
            try
            {
                _logger.LogInformation("Creating new reinforcer agent in database");
                await _context.ReinforcerAgents.AddAsync(agent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating reinforcer agent in database");
                throw;
            }
        }

        public async Task UpdateAsync(ReinforcerAgent agent)
        {
            try
            {
                _logger.LogInformation($"Updating reinforcer agent with id {agent.Id} in database");
                _context.ReinforcerAgents.Update(agent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating reinforcer agent with id {agent.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting reinforcer agent with id {id} from database");
                var agent = await _context.ReinforcerAgents.FindAsync(id);
                if (agent != null)
                {
                    _context.ReinforcerAgents.Remove(agent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting reinforcer agent with id {id} from database");
                throw;
            }
        }

    }
}
