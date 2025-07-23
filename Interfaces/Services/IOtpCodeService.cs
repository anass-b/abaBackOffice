using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IOtpCodeService
    {
        Task<OtpCodeDto> GetByCodeAsync(string code);
        Task<OtpCodeDto> CreateAsync(OtpCodeDto otpCodeDto);
        Task<bool> ValidateCodeAsync(string code, string email);
        Task DeleteAsync(int id);
    }
}