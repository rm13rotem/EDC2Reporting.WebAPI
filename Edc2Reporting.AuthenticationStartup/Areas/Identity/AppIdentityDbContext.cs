using DataServices.SqlServerRepository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Edc2Reporting.AuthenticationStartup.Areas.Identity
{
    public class AppIdentityDbContext
       : IdentityDbContext<Investigator, IdentityRole, string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }
    }

}
