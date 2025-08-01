namespace abaBackOffice.DTOs
{
    public class DomainDto : AuditableDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Prefix { get; set; }

        public int CategoryId { get; set; }
        
    }
}
