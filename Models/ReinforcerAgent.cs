using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("reinforceragents", Schema = "core")]
    public class ReinforcerAgent : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Column("type")]
        public string Type { get; set; }

        [MaxLength(50)]
        [Column("category")]
        public string Category { get; set; }

        [Column("previewurl")]
        public string PreviewUrl { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }
    }
}
