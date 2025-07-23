using abaBackOffice.DTOs;

public class ReinforcerAgentDto : AuditableDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Category { get; set; }
    public string PreviewUrl { get; set; }
    public bool IsActive { get; set; }
}