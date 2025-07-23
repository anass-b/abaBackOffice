using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IVideoService
    {
        Task<IEnumerable<VideoDto>> GetAllAsync();
        Task<VideoDto> GetByIdAsync(int id);
        Task<VideoDto> CreateAsync(VideoDto videoDto);
        Task<VideoDto> UpdateAsync(VideoDto videoDto);
        Task DeleteAsync(int id);
    }
}