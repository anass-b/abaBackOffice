// -------------------- SubscriptionRepository --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<SubscriptionRepository> _logger;

        public SubscriptionRepository(AbaDbContext context, ILogger<SubscriptionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all subscriptions from database");
                return await _context.Subscriptions
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subscriptions from database");
                throw;
            }
        }

        public async Task<Subscription> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving subscription with id {id} from database");
                return await _context.Subscriptions.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving subscription with id {id} from database");
                throw;
            }
        }

        public async Task CreateAsync(Subscription subscription)
        {
            try
            {
                _logger.LogInformation("Creating new subscription in database");
                await _context.Subscriptions.AddAsync(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subscription in database");
                throw;
            }
        }

        public async Task UpdateAsync(Subscription subscription)
        {
            try
            {
                _logger.LogInformation($"Updating subscription with id {subscription.Id} in database");
                _context.Subscriptions.Update(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating subscription with id {subscription.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting subscription with id {id} from database");
                var subscription = await _context.Subscriptions.FindAsync(id);
                if (subscription != null)
                {
                    _context.Subscriptions.Remove(subscription);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting subscription with id {id} from database");
                throw;
            }
        }

    }
}
