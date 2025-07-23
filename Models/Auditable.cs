using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abaBackOffice.Models
{
    public abstract class Auditable
    {
        [ForeignKey("CreatedBy")]
        public int Created_by { get; set; }
        public User CreatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public int? Updated_by { get; set; }
        public User? UpdatedBy { get; set; }

        private DateTime _created_at;
        public DateTime Created_at
        {
            get => _created_at;
            set => _created_at = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        private DateTime? _updated_at;
        public DateTime? Updated_at
        {
            get => _updated_at;
            set => _updated_at = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : (DateTime?)null;
        }

        [ConcurrencyCheck]
        [Column("rowversion")]
        public int RowVersion { get; set; } = 1;
    }
}