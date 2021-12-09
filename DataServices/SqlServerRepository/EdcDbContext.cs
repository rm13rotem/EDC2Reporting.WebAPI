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
            if (!string.IsNullOrWhiteSpace(ConnectionString))
                optionsBuilder.UseSqlServer(ConnectionString);
        }

        public virtual DbSet<Experiments> Experiments { get; set; }
        public virtual DbSet<ModuleInfos> ModuleInfos { get; set; }
        public virtual DbSet<PersistentEntity> PersistentEntity { get; set; }
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
