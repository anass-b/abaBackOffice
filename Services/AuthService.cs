using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using abaBackOffice.Helpers;


public class AuthService : IAuthService
{
    private static readonly ConcurrentDictionary<int, string> _refreshTokens = new();
    private static readonly object _lock = new();

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AuthService> _logger;
    private readonly string _passphrase;
    private readonly IEncryptionService _encryptionService;
    private readonly IMapper _mapper;
  


    public AuthService(
        IUnitOfWork unitOfWork,
        ILogger<AuthService> logger,
        IEncryptionService encryptionService,
        IOptions<EncryptionOptions> encryptionOptions,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _passphrase = encryptionOptions.Value.Passphrase;
        _encryptionService = encryptionService;
        _mapper = mapper;
    }

    public async Task<UserDto> AuthenticateAsync(string email, string password)
    {
        try
        {
            _logger.LogInformation($"Authenticating user with email {email}");

            var user = await _unitOfWork.UserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("User not found");
                return null;
            }

            if (!_encryptionService.CompareDecryptedWithEncrypted(password, user.PasswordHash, _passphrase))
            {
                _logger.LogWarning("Password does not match");
                return null;
            }
            _logger.LogDebug($"Passphrase value: '{_passphrase}'");

            if (string.IsNullOrWhiteSpace(_passphrase))
            {
                _logger.LogError("Passphrase is null or empty! Cannot decrypt passwords.");
                throw new InvalidOperationException("Encryption passphrase is not configured.");
            }

            return _mapper.Map<UserDto>(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error authenticating user with email {email}");
            throw;
        }
    }

    public Task SaveRefreshTokenAsync(int userId, string refreshToken)
    {
        lock (_lock)
        {
            _logger.LogInformation($"Saving refresh token for user ID {userId}");
            _refreshTokens[userId] = refreshToken;
        }
        return Task.CompletedTask;
    }

    public Task<string> GetRefreshTokenAsync(int userId)
    {
        lock (_lock)
        {
            _refreshTokens.TryGetValue(userId, out var refreshToken);
            return Task.FromResult(refreshToken);
        }
    }

    public async Task<bool> UpdatePasswordAsync(int userId, string newPassword)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User not found for password update");
                return false;
            }

            user.PasswordHash = _encryptionService.Encrypt(newPassword, _passphrase);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Password updated successfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating password for user ID: {userId}");
            throw;
        }
    }
}
