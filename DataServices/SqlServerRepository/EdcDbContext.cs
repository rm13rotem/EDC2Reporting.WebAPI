using DataServices.SqlServerRepository.Models;
using DataServices.SqlServerRepository.Models.CrfModels;
using DataServices.SqlServerRepository.Models.VisitAssembley;
using DataServices.SqlServerRepository.Models.VisitGroup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataServices.SqlServerRepository
{
    public partial class EdcDbContext : DbContext
    {
        public EdcDbContext()
        {
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly UserManager<Investigator> _userManager;
        public EdcDbContext(DbContextOptions<EdcDbContext> options, IHttpContextAccessor httpContextAccessor,
        UserManager<Investigator> userManager)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
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
                    optionsBuilder.UseSqlServer(
                        "Server=.\\SQLEXPRESS01;Initial Catalog=Edc_DEV;Integrated Security=True;" +
                        "Connect Timeout=30;Encrypt=true;TrustServerCertificate=True;" +
                        "ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }

        }


        public DbSet<CrfPage> CrfPages { get; set; }
        public DbSet<CrfEntry> CrfEntries { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public virtual DbSet<Experiment> Experiments { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<VisitGroup> VisitGroups { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

        public virtual DbSet<EmailModel> Emails { get; set; }

        //public virtual DbSet<Study> Studies { get; set; }
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            string userName = "System";

            if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (user != null)
                {
                    userName = $"{user.FirstName} {user.LastName}".Trim();
                }
            }

            foreach (var entry in ChangeTracker.Entries<CrfEntry>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.LastUpdator = userName;
                    entry.Entity.GuidId = Guid.NewGuid().ToString();
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.LastUpdator = userName;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
