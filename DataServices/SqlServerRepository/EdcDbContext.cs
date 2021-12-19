using DataServices.SqlServerRepository.Models;
using Microsoft.EntityFrameworkCore;

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

            OnModelCreatingPartial(modelBuilder);
        }
    

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
