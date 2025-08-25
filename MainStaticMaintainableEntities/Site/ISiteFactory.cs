using DataServices.SqlServerRepository.Models;

namespace MainStaticMaintainableEntities.SiteAssembly
{
    public interface ISiteFactory
    {
        Investigator LoadDoctorById(int siteManagerId);
    }
}