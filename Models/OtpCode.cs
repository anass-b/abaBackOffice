using abaBackOffice.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("otpcodes", Schema = "core")]
    public class OtpCode : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("userid")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required, MaxLength(6)]
        [Column("code")]
        public string Code { get; set; }

        [Column("expiresat")]
        public DateTime? ExpiresAt { get; set; }

        [Column("isused")]
        public bool IsUsed { get; set; } = false;
    }
}
