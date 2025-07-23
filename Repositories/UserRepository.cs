// -------------------- UserRepository --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AbaDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all users from database");
                return await _context.Users
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users from database");
                throw;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving user with id {id} from database");
                return await _context.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user with id {id} from database");
                throw;
            }
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                _logger.LogInformation($"Retrieving user with email {email} from database");
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user with email {email} from database");
                throw;
            }
            
        }


        public async Task CreateAsync(User user)
        {
            try
            {
                _logger.LogInformation("Creating new user in database");
                await _context.Users.AddAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user in database");
                throw;
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                _logger.LogInformation($"Updating user with id {user.Id} in database");
                _context.Users.Update(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user with id {user.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting user with id {id} from database");
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user with id {id} from database");
                throw;
            }
        }

    }
}
