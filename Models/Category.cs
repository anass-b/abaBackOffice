using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("categories", Schema = "core")]
    public class Category : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }
        public ICollection<Domain> Domains { get; set; } = new List<Domain>();

    }

}
