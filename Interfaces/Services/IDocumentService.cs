using abaBackOffice.DTOs;

namespace abaBackOffice.Interfaces.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentDto>> GetAllAsync();
        Task<DocumentDto> GetByIdAsync(int id);
        Task<DocumentDto> CreateAsync(DocumentDto documentDto);
        Task<DocumentDto> UpdateAsync(DocumentDto documentDto);
        Task DeleteAsync(int id);
    }
}