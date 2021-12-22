namespace MainStaticMaintainableEntities.SiteAssembly
{
    public interface ISiteFactory
    {
        Doctor LoadDoctorById(int siteManagerId);
    }
}