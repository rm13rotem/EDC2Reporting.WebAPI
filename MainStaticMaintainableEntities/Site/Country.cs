using DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace MainStaticMaintainableEntities.SiteAssembly
{
    public class Country : IPersistentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }

        public string NameShort { get; set; }
        public string Languages { get; set; }

        public static SelectList GetCountrySelectList(IRepository<Country> countryRepository, int CountryId)
        {
            return new SelectList(countryRepository.GetAll(), "Id", "Name", CountryId);
        }
    }
}
