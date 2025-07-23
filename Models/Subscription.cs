using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("subscriptions", Schema = "core")]
    public class Subscription : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("userid")]
        public int UserId { get; set; }
        public User User { get; set; }

        [MaxLength(50)]
        [Column("type")]
        public string Type { get; set; }

        [Column("startdate")]
        public DateTime? StartDate { get; set; }

        [Column("enddate")]
        public DateTime? EndDate { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }
    }
}
