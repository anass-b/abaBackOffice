using abaBackOffice.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("documents", Schema = "core")]
    public class Document : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(200)]
        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("categories", TypeName = "text[]")]
        public List<string>? Categories { get; set; }


        [Column("fileurl")]
        public string FileUrl { get; set; }

        [Column("ispremium")]
        public bool IsPremium { get; set; } = false;
    }
}
