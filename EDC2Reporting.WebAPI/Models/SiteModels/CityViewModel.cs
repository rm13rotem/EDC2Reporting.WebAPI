using DataServices.Interfaces;
using MainStaticMaintainableEntities.SiteAssembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models.SiteModels
{
    public class CityViewModel
    {
        public CityViewModel(City c)
        {
            _c = c;
            Name = c.Name;
            CountryName = c.CountryId == 0 ? "Not Selected" : 
                CountryRepository?.GetById(c.CountryId)?.Name;
            Id = c.Id;
            GuidId = c.GuidId;
            Code = c.CountryId == 0 ? "Not Selected" :
                CountryRepository?.GetById(c.CountryId)?.NameShort;
            IsDeleted = c.IsDeleted;
        }

        public static IRepository<Country> CountryRepository { get; internal set; }
        public City _c { get; private set; }
        public string Name { get; private set; }
        public string CountryName { get; private set; }
        public int Id { get; private set; }
        public string GuidId { get; private set; }
        public string Code { get; private set; }
        public bool IsDeleted { get; private set; }
    }
}
