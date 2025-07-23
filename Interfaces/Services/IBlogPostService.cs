using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostDto>> GetAllAsync();
        Task<BlogPostDto> GetByIdAsync(int id);
        Task<BlogPostDto> CreateAsync(BlogPostDto dto);
        Task<BlogPostDto> UpdateAsync(BlogPostDto dto);
        Task DeleteAsync(int id);
    }
}