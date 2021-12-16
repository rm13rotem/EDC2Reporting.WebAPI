using DataServices.Interfaces;
using DataServices.Providers;
using MainStaticMaintainableEntities.VisitAssembly;
using System.Collections.Generic;
using Utilities;

namespace MainStaticMaintainableEntities.VisitGroup
{
    public class VisitGroup : IPersistentEntity
    {
        public List<VisitGroup> VisitGroups { get; set; }
        public List<Visit> Visits { get; set; }
        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string JsonValue { get; set; }
        public string Name { get; set; }

          
        public void LoadFromPersistentEntity(IPersistentEntity persistentEntity)
        {
            var loaded = JsonFormatter.FromJson<VisitGroup>(persistentEntity.JsonValue);
            if (loaded.VisitGroups != null) this.VisitGroups = loaded.VisitGroups;
            else if (loaded.Visits != null) this.Visits = loaded.Visits;
        }


        public int NumberMyVisitsInternally(int visitInternalId = 0)
        {
            visitInternalId++;
            if (visitInternalId > 1000)
                return visitInternalId;
            if (VisitGroups == null)
            {
                foreach (var v in Visits)
                {
                    v.InternalIndex = visitInternalId++;
                }
                return visitInternalId;
            }
            else
            {
                foreach (var visit_group in VisitGroups)
                {
                    visitInternalId = visit_group.NumberMyVisitsInternally(visitInternalId);
                }
                return visitInternalId;
            }
        }
    }
}
