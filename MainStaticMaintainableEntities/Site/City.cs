using DataServices.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MainStaticMaintainableEntities.SiteAssembly
{
    public class City : IPersistentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }

        public static SelectList GetCitySelectList(IRepository<City> cityRepository, int CountryId, int CityId)
        {
            return new SelectList(cityRepository.GetAll().Where(x=>x.CountryId == CountryId).ToList(),
                "Id", "Name", CityId);
        }
    }
}
