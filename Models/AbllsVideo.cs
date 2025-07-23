using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("abllsvideos", Schema = "core")]
    public class AbllsVideo : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("taskid")]
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public AbllsTask Task { get; set; }

        [MaxLength(50)]
        [Column("type")]
        public string Type { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("thumbnailurl")]
        public string ThumbnailUrl { get; set; }
    }
}
