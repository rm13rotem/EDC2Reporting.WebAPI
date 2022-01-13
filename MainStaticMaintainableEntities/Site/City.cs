using DataServices.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.Site
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
    }
}
