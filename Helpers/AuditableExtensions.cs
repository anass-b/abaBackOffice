using abaBackOffice.Models;

namespace abaBackOffice.Helpers
{
    public static class AuditableExtensions
    {
        public static void ConvertDatesToUtc(this Auditable entity)
        {
            if (entity == null) return;

            entity.Created_at = DateTime.SpecifyKind(entity.Created_at, DateTimeKind.Utc);
            if (entity.Updated_at.HasValue)
            {
                entity.Updated_at = DateTime.SpecifyKind(entity.Updated_at.Value, DateTimeKind.Utc);
            }
        }

        public static void ConvertDatesToUtc(this IEnumerable<Auditable> entities)
        {
            foreach (var entity in entities)
            {
                entity.ConvertDatesToUtc();
            }
        }
    }
}