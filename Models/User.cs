using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("users", Schema = "core")]
    public class User : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("passwordhash")]
        public string PasswordHash { get; set; }

        [MaxLength(100)]
        [Column("firstname")]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [Column("lastname")]
        public string LastName { get; set; }

        [MaxLength(20)]
        [Column("phonenumber")]
        public string PhoneNumber { get; set; }

        [Column("isverified")]
        public bool IsVerified { get; set; } = false;

        [Column("isadmin")]
        public bool IsAdmin { get; set; } = false;

        public ICollection<OtpCode> OtpCodes { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        public ICollection<ReinforcementProgram> ReinforcementPrograms { get; set; }
    }
}
