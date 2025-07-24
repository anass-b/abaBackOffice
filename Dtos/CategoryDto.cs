namespace abaBackOffice.DTOs
{
    public class CategoryDto : AuditableDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }
    }
}
