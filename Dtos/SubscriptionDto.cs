using abaBackOffice.DTOs;

public class SubscriptionDto : AuditableDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Type { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
}
