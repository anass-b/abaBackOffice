using abaBackOffice.DTOs;

public class BlogCommentDto : AuditableDto
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int? UserId { get; set; }
    public string Content { get; set; }
    
}