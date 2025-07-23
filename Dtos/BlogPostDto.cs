using abaBackOffice.DTOs;

public class BlogPostDto : AuditableDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int? AuthorId { get; set; }
    public string Category { get; set; }
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; }
}