using abaBackOffice.DTOs;

public class DocumentDto : AuditableDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string>? Categories { get; set; }
    public string? FileUrl { get; set; }
    public bool IsPremium { get; set; }
    public IFormFile File { get; set; }

}