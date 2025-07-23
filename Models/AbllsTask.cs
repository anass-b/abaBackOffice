using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("abllstasks", Schema = "core")]
    public class AbllsTask : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(20)]
        [Column("reference")]
        public string Reference { get; set; }

        [Required, MaxLength(255)]
        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [MaxLength(100)]
        [Column("domain")]
        public string Domain { get; set; }

        [Column("baselinetext")]
        public string BaselineText { get; set; }

        [Column("baselinevideo")]
        public string BaselineVideo { get; set; }

        [Column("materials", TypeName = "jsonb")]
        public string[] Materials { get; set; }  // Requires Npgsql + configuration for EF Core

        [Column("instructions")]
        public string Instructions { get; set; }

        public ICollection<AbllsVideo> AbllsVideos { get; set; }
    }
}
