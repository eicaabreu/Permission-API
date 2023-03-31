using Microsoft.EntityFrameworkCore;
using Permission.Infra.Data.Models;

namespace Permission.Infra.Data.Context
{
    public partial class PermissionDBContext : DbContext
    {

        public PermissionDBContext() { }

        public PermissionDBContext(DbContextOptions<PermissionDBContext> options)
           : base(options)
        {
        }
        public virtual DbSet<Permission.Infra.Data.Models.Permission> Permission { get; set; }
        public virtual DbSet<PermissionType> PermissionType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Permission.Infra.Data.Models.Permission>(entity =>
            {
                entity.ToTable("permissions");

                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

                entity.Property(e => e.DeletedAt).HasDefaultValueSql("now()");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PermissionTypeId);

            });

            modelBuilder.Entity<PermissionType>(entity =>
            {
                entity.ToTable("permission_types");

                entity.Property(e => e.Description).IsRequired();

            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
