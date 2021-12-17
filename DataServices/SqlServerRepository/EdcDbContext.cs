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
            modelBuilder.Entity<PersistentEntity>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
