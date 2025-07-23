using abaBackOffice.DTOs;

public class AbllsVideoDto : AuditableDto
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public string ThumbnailUrl { get; set; }
}