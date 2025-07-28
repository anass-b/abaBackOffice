// DbContext for ABA schema
using abaBackOffice.Configurations;
using abaBackOffice.Interfaces.Services;
using abaBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace abaBackOffice.DataAccessLayer
{
    public class AbaDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public AbaDbContext(DbContextOptions<AbaDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OtpCode> OtpCodes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AbllsTask> AbllsTasks { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<ReinforcementProgram> ReinforcementPrograms { get; set; }
        public DbSet<ReinforcerAgent> ReinforcerAgents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EvaluationCriteria> EvaluationCriterias { get; set; }
        public DbSet<MaterialPhoto> MaterialPhotos { get; set; }
        public DbSet<BaselineContent> BaselineContents { get; set; }
        public DbSet<EvaluationCriteriaMaterial> EvaluationCriteriaMaterials { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<User>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<OtpCode>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<Subscription>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<Video>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<Document>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<AbllsTask>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<BlogPost>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<BlogComment>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<ReinforcementProgram>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<ReinforcerAgent>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<Category>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<EvaluationCriteria>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<MaterialPhoto>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<BaselineContent>());
            modelBuilder.ApplyConfiguration(new AuditableEntityConfiguration<EvaluationCriteriaMaterial>());

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower());
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToLower());
                }
            }
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Auditable && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (Auditable)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.Created_at = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
                    entity.Created_by = _currentUserService.GetCurrentUserId();
                    entity.Updated_at = null;
                    entity.Updated_by = null;
                    entity.RowVersion = 1;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.Updated_at = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
                    entity.Updated_by = _currentUserService.GetCurrentUserId();
                    entity.RowVersion++;

                    entry.Property(nameof(Auditable.Created_by)).IsModified = false;
                    entry.Property(nameof(Auditable.Created_at)).IsModified = false;
                }
            }
        }
    }
}