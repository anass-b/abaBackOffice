using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.Extensions.Logging;

namespace abaBackOffice.Services
{
    public class OtpCodeService : IOtpCodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OtpCodeService> _logger;
        private readonly IMapper _mapper;

        public OtpCodeService(IUnitOfWork unitOfWork, ILogger<OtpCodeService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<OtpCodeDto> GetByCodeAsync(string code)
        {
            try
            {
                var otp = await _unitOfWork.OtpCodeRepository.GetByCodeAsync(code);
                return otp != null ? _mapper.Map<OtpCodeDto>(otp) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la récupération du code OTP: {code}");
                throw;
            }
        }

        public async Task<OtpCodeDto> CreateAsync(OtpCodeDto dto)
        {
            try
            {
                var entity = _mapper.Map<OtpCode>(dto);
                await _unitOfWork.OtpCodeRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<OtpCodeDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création d’un code OTP");
                throw;
            }
        }

        public async Task<bool> ValidateCodeAsync(string code, string email)
        {
            try
            {
                var otp = await _unitOfWork.OtpCodeRepository.GetByCodeAsync(code);

                if (otp == null || otp.IsUsed || otp.ExpiresAt < DateTime.UtcNow)
                    return false;

                if (!string.Equals(otp.User?.Email, email, StringComparison.OrdinalIgnoreCase))
                    return false;

                otp.IsUsed = true;
                await _unitOfWork.OtpCodeRepository.UpdateAsync(otp);
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la validation du code OTP");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.OtpCodeRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la suppression du code OTP avec l’id {id}");
                throw;
            }
        }
    }
}
