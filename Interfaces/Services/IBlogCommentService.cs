using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IBlogCommentService
    {
        Task<IEnumerable<BlogCommentDto>> GetAllAsync();
        Task<BlogCommentDto> GetByIdAsync(int id);
        Task<BlogCommentDto> CreateAsync(BlogCommentDto dto);
        Task<BlogCommentDto> UpdateAsync(BlogCommentDto dto);
        Task DeleteAsync(int id);
    }
}