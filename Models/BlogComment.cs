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

        [Column("postid")]
        public int PostId { get; set; }
        public BlogPost Post { get; set; }

        [Column("userid")]
        public int? UserId { get; set; }
        public User User { get; set; }

        [Column("content")]
        public string Content { get; set; }
    }
}
