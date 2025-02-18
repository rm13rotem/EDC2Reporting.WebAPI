using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edc2Reporting.AuthenticationStartup.Areas.PersistentEntities.Models
{
    public interface IPersistantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
