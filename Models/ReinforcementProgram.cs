using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    [Table("reinforcementprograms", Schema = "core")]
    public class ReinforcementProgram : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("userid")]
        public int? UserId { get; set; }
        public User User { get; set; }

        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Column("type")]
        public string Type { get; set; }

        [Column("value")]
        public int? Value { get; set; }

        [MaxLength(20)]
        [Column("unit")]
        public string Unit { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
