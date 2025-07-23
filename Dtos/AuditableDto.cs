using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.DTOs
{
    public class AuditableDto
    {
        public int Created_by { get; set; }
        public DateTime Created_at { get; set; }
        public int Updated_by { get; set; }
        public DateTime? Updated_at { get; set; }
        public int RowVersion { get; set; }
    }
}
