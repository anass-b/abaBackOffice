using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("domains", Schema = "core")]
    public class Domain : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required, MaxLength(10)]
        [Column("prefix")]
        public string Prefix { get; set; }  // ex: A, B, C

        [Required]
        [ForeignKey("Category")]
        [Column("category_id")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
