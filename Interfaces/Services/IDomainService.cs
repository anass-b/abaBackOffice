using abaBackOffice.DTOs;

public interface IDomainService
{
    Task<IEnumerable<DomainDto>> GetAllAsync();
    Task<DomainDto> GetByIdAsync(int id);
    Task<IEnumerable<DomainDto>> GetByCategoryIdAsync(int categoryId);
    Task<DomainDto> CreateAsync(DomainDto dto);
    Task<DomainDto> UpdateAsync(DomainDto dto);
    Task DeleteAsync(int id);
}
