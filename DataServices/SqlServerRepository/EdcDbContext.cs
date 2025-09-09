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
                    optionsBuilder.UseSqlServer("Server=LAPTOP-6ORP7UKE\\SQLEXPRESS;Initial Catalog=EDC_Dev;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }

        }


        public DbSet<CrfPage> CrfPages { get; set; }
        public DbSet<CrfEntry> CrfEntries { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public virtual DbSet<Experiment> Experiments { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<VisitGroup> VisitGroups { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

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
                entity.Property(e => e.GuidId).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FormDataJson).IsRequired(); ;
                entity.Property(e => e.StudyId).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IsDeleted).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.LastUpdator).HasDefaultValueSql("Auto generated")
                .IsRequired()
                .HasMaxLength(200);

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
