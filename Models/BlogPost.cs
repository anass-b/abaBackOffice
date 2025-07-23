using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("blogposts", Schema = "core")]
    public class BlogPost : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("title")]
        public string Title { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("author_id")] 
        public int? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public User Author { get; set; }

        [MaxLength(100)]
        [Column("category")]
        public string Category { get; set; }

        [Column("publishedat")]
        public DateTime? PublishedAt { get; set; }

        [Column("ispublished")]
        public bool IsPublished { get; set; }
    }
}
