using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(UserDto userDto);
        Task<UserDto> UpdateAsync(UserDto userDto);
        Task DeleteAsync(int id);
    }
}