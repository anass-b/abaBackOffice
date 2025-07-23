using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace abaBackOffice.Configurations
{
    public class AuditableEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Auditable
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            // Configure RowVersion as a concurrency token
            builder.Property<int>("RowVersion")
                   .IsConcurrencyToken()
                   .HasDefaultValue(1);

            // Configure relationships for CreatedBy and UpdatedBy
            builder.HasOne(a => a.CreatedBy)
                   .WithMany()
                   .HasForeignKey(a => a.Created_by)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.UpdatedBy)
                   .WithMany()
                   .HasForeignKey(a => a.Updated_by)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
