using abaBackOffice.DTOs;

public class AbllsTaskDto : AuditableDto
{
    public int Id { get; set; }
    public string Reference { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Domain { get; set; }
    public string BaselineText { get; set; }
    public string BaselineVideo { get; set; }
    public string[] Materials { get; set; }
    public string Instructions { get; set; }
}