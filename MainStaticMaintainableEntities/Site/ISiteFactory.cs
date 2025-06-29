namespace MainStaticMaintainableEntities.SiteAssembly
{
    public interface ISiteFactory
    {
        Investigator LoadDoctorById(int siteManagerId);
    }
}