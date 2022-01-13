using DataServices.Interfaces;
using DataServices.Providers;
using Newtonsoft.Json;

namespace MainStaticMaintainableEntities.SiteAssembly
{
    public class Site : IPersistentEntity
    {
        [JsonIgnore]
        public Doctor SiteManager
        {
            get
            {
                var repositoryOptions = new RepositoryOptions()
                {
                    RepositoryType = RepositoryType.FromDbRepository,
                    IsOverwritingOtherRepositories = false
                };
                SiteFactory siteFactory = new SiteFactory(repositoryOptions);
                var siteDoctor = siteFactory.LoadDoctorById(SiteManagerId);
                return siteDoctor;
            }
        }
        public int SiteManagerId { get; set; }

        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
    }
}
