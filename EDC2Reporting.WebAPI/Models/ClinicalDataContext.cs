using MainStaticMaintainableEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models
{
    public class ClinicalDataContext : DbContext
    {
        public ClinicalDataContext(DbContextOptions<ClinicalDataContext> options) : base(options)
        {

        }

        public DbSet<ModuleData> ModuleInfos { get; set; }
        public DbSet<PersistantEntity> PersistantEntities { get; set; }
        public DbSet<Experiment> Experiments { get; set; }
    }
}
