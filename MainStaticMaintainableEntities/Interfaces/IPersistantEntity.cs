using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.Interfaces
{
    public interface IPersistantEntity
    {
        // a persistant entity is one that is stored in the DB, or in memory, or on a json file, or on an XML file
        // classic examples are a Site, a Doctor, a Patient, a Visit, a VisitGroup, a City, a Country. Typically these are small and never
        // change. or change rarely.
        int Id { get; set; }
        public string GuidId { get; set; }
        public string EntityName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string JsonValue { get; set; }
    }
}
