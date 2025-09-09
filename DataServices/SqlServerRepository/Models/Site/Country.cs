using DataServices.Interfaces;

namespace DataServices.SqlServerRepository.Models.Site
{
    public class Country : IPersistentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }

        public string NameShort { get; set; }
        public string Languages { get; set; }
       
    }
}
