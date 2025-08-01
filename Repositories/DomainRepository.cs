using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Repositories
{
    public class DomainRepository : IDomainRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<DomainRepository> _logger;

        public DomainRepository(AbaDbContext context, ILogger<DomainRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Domain>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all domains");
                return await _context.Domains
                    .Include(d => d.Category)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving domains");
                throw;
            }
        }

        public async Task<Domain?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving domain with id {id}");
                return await _context.Domains
                    .Include(d => d.Category)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving domain with id {id}");
                throw;
            }
        }

        public async Task<IEnumerable<Domain>> GetByCategoryIdAsync(int categoryId)
        {
            try
            {
                _logger.LogInformation($"Retrieving domains for categoryId {categoryId}");
                return await _context.Domains
                    .Where(d => d.CategoryId == categoryId)
                    .Include(d => d.Category)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving domains for categoryId {categoryId}");
                throw;
            }
        }

        public async Task CreateAsync(Domain domain)
        {
            try
            {
                _logger.LogInformation("Creating domain");
                await _context.Domains.AddAsync(domain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating domain");
                throw;
            }
        }

        public async Task UpdateAsync(Domain domain)
        {
            try
            {
                _logger.LogInformation($"Updating domain with id {domain.Id}");
                _context.Domains.Update(domain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating domain with id {domain.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting domain with id {id}");
                var domain = await _context.Domains.FindAsync(id);
                if (domain != null)
                    _context.Domains.Remove(domain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting domain with id {id}");
                throw;
            }
        }

        public IQueryable<Domain> GetQueryable()
        {
            return _context.Domains.AsQueryable();
        }
    }
}
