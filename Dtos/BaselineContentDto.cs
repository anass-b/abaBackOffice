using abaBackOffice.DTOs;
namespace abaBackOffice.DTOs;
public class BaselineContentDto : AuditableDto
{
    public int? Id { get; set; }

    public int? AbllsTaskId { get; set; }
    public int? CriteriaId { get; set; }

    public string? ContentHtml { get; set; }

    public IFormFile? File { get; set; }         // PDF ou image
    public string? FileUrl { get; set; }
}
