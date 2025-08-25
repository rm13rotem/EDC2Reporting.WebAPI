using DataServices.SqlServerRepository.Models;
using DataServices.SqlServerRepository.Models.CrfModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataServices.SqlServerRepository
{
    public partial class EdcDbContext : DbContext
    {
        public EdcDbContext()
        {
        }

        public EdcDbContext(DbContextOptions<EdcDbContext> options)
            : base(options)
        {
        }

        public EdcDbContext(string connString) : base()
        {
            ConnectionString = connString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (!string.IsNullOrWhiteSpace(ConnectionString))
                    optionsBuilder.UseSqlServer(ConnectionString);
                else
                optionsBuilder.UseSqlServer("Server=LAPTOP-6ORP7UKE\\SQLEXPRESS;Initial Catalog=EDC_Dev;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
            
        }


        public DbSet<CrfPage> CrfPages { get; set; }
        public  DbSet<CrfEntry> CrfEntries { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public virtual DbSet<Experiment> Experiments { get; set; }
        public virtual DbSet<ModuleInfo> ModuleInfos { get; set; }
        public virtual DbSet<PersistentEntity> PersistentEntities { get; set; }
        public string ConnectionString { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Experiment>(entity =>
            {
                entity.Property(e => e.CompanyName).HasMaxLength(1000);

                entity.Property(e => e.HelsinkiApprovalNumber).HasMaxLength(1000);

                entity.Property(e => e.UniqueIdentifier).HasMaxLength(1000);
            });

            modelBuilder.Entity<ModuleInfo>(entity =>
            {
                entity.Property(e => e.CurrentLastUpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DataInJson).IsRequired();
            });

            modelBuilder.Entity<PersistentEntity>(entity =>
            {
                entity.ToTable("PersistentEntity");

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

            // NEW: CrfPage
            modelBuilder.Entity<CrfPage>(entity =>
            {
                entity.ToTable("CrfPages");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Html).IsRequired();
            });

            // NEW: CrfEntry
            modelBuilder.Entity<CrfEntry>(entity =>
            {
                entity.ToTable("CrfEntries");
                entity.Property(e => e.CrfPageId).IsRequired();
                entity.Property(e => e.StudyId).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.IsDeleted).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(d => d.CrfPage)
                       .WithMany(p => p.Entries)
                      .HasForeignKey(d => d.CrfPageId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // NEW: AuditLog
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLogs");
                entity.Property(e => e.Action).IsRequired().HasMaxLength(100);
                entity.Property(e => e.EntityName).HasMaxLength(4000);
                entity.Property(e => e.UserId).HasMaxLength(40);
                entity.Property(e => e.BeforeJson).HasMaxLength(4000);
                entity.Property(e => e.AfterJson).HasMaxLength(4000);
                entity.Property(e => e.MetadataJson).IsRequired().HasMaxLength(500);
                entity.Property(e => e.TimestampUtc).HasDefaultValueSql("GETDATE()");
            });
            OnModelCreatingPartial(modelBuilder);
        }
    

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
