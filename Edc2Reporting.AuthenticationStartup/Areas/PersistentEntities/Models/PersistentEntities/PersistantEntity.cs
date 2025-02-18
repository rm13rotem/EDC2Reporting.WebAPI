using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edc2Reporting.AuthenticationStartup.Areas.PersistentEntities.Models
{
    /// <summary>
    /// The purpose of this abstract class is small entities, usually 
    /// persisted to a small json text file and referred to 
    /// using an in memory repository, Examples - Research sites, 
    /// experiment types, experiment stages, common key/value
    /// collections, and so on.
    /// </summary>
    public class PersistantEntity : IPersistantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
