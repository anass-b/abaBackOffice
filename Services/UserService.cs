// AutoMapper-based Services for ABA project
using abaBackOffice.DTOs;
using abaBackOffice.Helpers;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace abaBackOffice.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly string _passphrase;

        public UserService(IUnitOfWork unitOfWork, IOptions<EncryptionOptions> encryptionOptions, ILogger<UserService> logger, IMapper mapper , IEncryptionService encryptionService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _passphrase = encryptionOptions.Value.Passphrase;
            _encryptionService = encryptionService;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<UserDto>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users");
                throw;
            }
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user with id {id}");
                throw;
            }
        }

        public async Task<UserDto> CreateAsync(UserDto dto)
        {
            try
            {
                dto.PasswordHash = _encryptionService.Encrypt(dto.PasswordHash, _passphrase);

                var entity = _mapper.Map<User>(dto);
                await _unitOfWork.UserRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<UserDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }

        public async Task<UserDto> UpdateAsync(UserDto dto)
        {
            try
            {
                var entity = _mapper.Map<User>(dto);
                await _unitOfWork.UserRepository.UpdateAsync(entity);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<UserDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user with id {dto.Id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting user with id {id} from database");
                await _unitOfWork.UserRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user with id {id} from database");
                throw;
            }
        }

    }

    // Repeat similarly for other services like VideoService, DocumentService, etc.
}
