using DataServices.Interfaces;
using DataServices.Providers;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;

namespace MainStaticMaintainableEntities.SiteAssembly
{
    public class Site : IPersistentEntity
    {
        private RepositoryOptions repositoryOptions = new RepositoryOptions()
        {
            RepositoryType = RepositoryType.FromDbRepository,
            IsOverwritingOtherRepositories = false
        };

        [JsonIgnore]
        public Doctor SiteManager
        {
            get
            {
                SiteFactory siteFactory = new SiteFactory(repositoryOptions);
                var siteDoctor = siteFactory.LoadDoctorById(SiteManagerId);
                return siteDoctor;
            }
        }

        [JsonIgnore]
        public City City
        {
            get
            {
                SiteFactory siteFactory = new SiteFactory(repositoryOptions);
                var city = siteFactory.LoadCityById(SiteManagerId);
                return city;
            }
        }
        [JsonIgnore]
        public Country Country
        {
            get
            {
                SiteFactory siteFactory = new SiteFactory(repositoryOptions);
                var siteDoctor = siteFactory.LoadCountryById(CountryId);
                return siteDoctor;
            }
        }

        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int SiteManagerId { get; set; }

        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }

        public static SelectList GetCitySelectList(IRepository<Site> siteRepository, 
            int CountryId, int CityId, int SiteId)
        {
            return new SelectList(siteRepository.GetAll().Where(x => x.CountryId == CountryId &&
            x.CityId == CityId).ToList(),
                "Id", "Name", SiteId);
        }
    }
}
