using DataServices.SqlServerRepository.Models;

namespace DataServices.SqlServerRepository.Models.Site
{
    public interface ISiteFactory
    {
        Investigator LoadDoctorById(int siteManagerId);
    }
}