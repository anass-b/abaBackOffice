namespace abaBackOffice.Interfaces.Services
{
    public interface IAuthService
    {
        Task<UserDto> AuthenticateAsync(string email, string password);
        Task SaveRefreshTokenAsync(int userId, string refreshToken);
        Task<string> GetRefreshTokenAsync(int userId);
        Task<bool> UpdatePasswordAsync(int userId, string newPassword);

    }
}
