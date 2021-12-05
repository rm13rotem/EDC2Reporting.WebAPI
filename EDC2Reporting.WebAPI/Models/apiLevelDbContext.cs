using MainStaticMaintainableEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models
{
    public class ApiLevelDbContext : ClinicalDataContext

    {
        
        public ApiLevelDbContext()
        {
        }

        public ApiLevelDbContext(DbContextOptions<ClinicalDataContext> options)
            : base(options)
        {
        }
    }
}
