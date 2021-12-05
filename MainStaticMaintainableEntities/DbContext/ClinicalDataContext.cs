using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MainStaticMaintainableEntities
{
    public partial class ClinicalDataContext : DbContext
    {
        public ClinicalDataContext()
        {
        }

        public ClinicalDataContext(DbContextOptions<ClinicalDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Experiments> Experiments { get; set; }
        public virtual DbSet<ModuleInfos> ModuleInfos { get; set; }
        public virtual DbSet<PersistentEntity> PersistentEntity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
// log error;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Experiments>(entity =>
            {
                entity.Property(e => e.CompanyName).HasMaxLength(1000);

                entity.Property(e => e.HelsinkiApprovalNumber).HasMaxLength(1000);

                entity.Property(e => e.UniqueIdentifier).HasMaxLength(1000);
            });

            modelBuilder.Entity<ModuleInfos>(entity =>
            {
                entity.Property(e => e.CurrentLastUpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DataInJson).IsRequired();
            });

            modelBuilder.Entity<PersistentEntity>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.GuidId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.JsonValue).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
