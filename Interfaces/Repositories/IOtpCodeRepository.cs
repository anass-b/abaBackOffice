using abaBackOffice.Models;

namespace abaBackOffice.Interfaces.Repositories
{
    public interface IOtpCodeRepository
    {
        Task<OtpCode?> GetByCodeAsync(string code);
        Task CreateAsync(OtpCode entity);
        Task UpdateAsync(OtpCode entity);
        Task<OtpCode?> GetByIdAsync(int id);
        Task<IEnumerable<OtpCode>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
