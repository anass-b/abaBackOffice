using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("blogcomments", Schema = "core")]
    public class BlogComment : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("post_id")]
        public int PostId { get; set; }
        public BlogPost Post { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }
        public User User { get; set; }

        [Column("content")]
        public string Content { get; set; }
    }
}
