using DataServices.Interfaces;
using MainStaticMaintainableEntities.SiteAssembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models.SiteModels
{
    public class SiteViewModel
    {
        public SiteViewModel(Site x)
        {
            Id = x.Id;
            CountryName = x.CountryId == 0 ? "Not Selected" :
                CountryRepository?.GetById(x.CountryId)?.Name;
            CityName = x.CityId == 0 ? "NotSelected" :
                CityRepository?.GetById(x.CityId)?.Name;
            SiteName = x.Name;
            IsDeleted = x.IsDeleted;
        }

        public static IRepository<City> CityRepository { get; internal set; }
        public static IRepository<Country> CountryRepository { get; internal set; }
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string SiteName { get; set; }
        public bool IsDeleted { get; private set; }
    }
}
