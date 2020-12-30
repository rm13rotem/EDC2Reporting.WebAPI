using MainStaticMaintainableEntities;
using MainStaticMaintainableEntities.ModuleAssembly;
using MainStaticMaintainableEntities.PatientAssembley;
using MainStaticMaintainableEntities.SiteAssembly;
using MainStaticMaintainableEntities.VisitAssembly;
using MainStaticMaintainableEntities.VisitGroup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models
{
    public class MockDbContext : DbContext
    {
        public MockDbContext(DbContextOptions<MockDbContext> options) : base(options)
        {
        }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<VisitGroup> VisitGroups { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Patient> Patients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Site>().HasData(
                new Site
                {
                    Id = 1,
                    Name = "Electronics",
                    EntityName = "site",
                }, 
                new Site
                {
                    Id = 2,
                    Name = "Test2",
                    EntityName = "site",
                },
                new Site
                {
                    Id = 3,
                    Name = "Test3",
                    EntityName = "site",
                },
                new Site
                {
                    Id = 4,
                    Name = "Test Grocery",
                    EntityName = "site",
                }
            );
        }

    }
}
