// -------------------- OtpCodeRepository --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.Repositories
{
    public class OtpCodeRepository : IOtpCodeRepository
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<OtpCodeRepository> _logger;

        public OtpCodeRepository(AbaDbContext context, ILogger<OtpCodeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<OtpCode>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all OTP codes from database");
                return await _context.OtpCodes
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving OTP codes from database");
                throw;
            }
        }
        public async Task<OtpCode?> GetByCodeAsync(string code)
        {
            try
            {
                _logger.LogInformation($"Retrieving OTP code by code: {code}");
                return await _context.OtpCodes
                    .Include(o => o.User) // pour accéder à l’email dans le service
                    .FirstOrDefaultAsync(o => o.Code == code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving OTP code with code {code} from database");
                throw;
            }
        }


        public async Task<OtpCode> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving OTP code with id {id} from database");
                return await _context.OtpCodes.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving OTP code with id {id} from database");
                throw;
            }
        }

        public async Task CreateAsync(OtpCode otpCode)
        {
            try
            {
                _logger.LogInformation("Creating new OTP code in database");
                await _context.OtpCodes.AddAsync(otpCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating OTP code in database");
                throw;
            }
        }

        public async Task UpdateAsync(OtpCode otpCode)
        {
            try
            {
                _logger.LogInformation($"Updating OTP code with id {otpCode.Id} in database");
                _context.OtpCodes.Update(otpCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating OTP code with id {otpCode.Id} in database");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting OTP code with id {id} from database");
                var otp = await _context.OtpCodes.FindAsync(id);
                if (otp != null)
                {
                    _context.OtpCodes.Remove(otp);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting OTP code with id {id} from database");
                throw;
            }
        }

    }
}
