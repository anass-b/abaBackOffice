using abaBackOffice.DTOs;

public class ReinforcementProgramDto : AuditableDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int? Value { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }
}