using Microsoft.EntityFrameworkCore;

namespace MainStaticMaintainableEntities
{
    public class ClinicalDataContext : DbContext
    {
        public ClinicalDataContext(DbContextOptions<ClinicalDataContext> options) : base(options)
        {

        }

        public DbSet<ModuleData> ModuleInfos { get; set; }
        public DbSet<PersistantEntity> PersistantEntities { get; set; }
        public DbSet<Experiment> Experiments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Experiment>().HasData(
                new Experiment()
                {
                    Id = 1,
                    Name = "Some1",
                    CompanyId = 1,
                    CompanyName = "Sundance"
                }, 
                new Experiment()
                {
                    Id = 2,
                    Name = "experiment2",
                    CompanyId = 1,
                    CompanyName = "Mel Devices"
                }
                );
        }
    }
}
