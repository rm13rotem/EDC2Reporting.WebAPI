using MainStaticMaintainableEntities.Providers;
using MainStaticMaintainableEntities.VisitAssembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace MainStaticMaintainableEntities.VisitGroup
{
    public class VisitGroup : PersistantEntity
    {
        private List<VisitGroup> _visitGroups;
        private List<Visit> _visits;

        public List<VisitGroup> VisitGroups
        {
            get { return _visitGroups; }
            set
            {
                if (value != null)
                    _visits = null;
                _visitGroups = value;
            }
        }
        public List<Visit> Visits
        {
            get { return _visits; }
            set
            {
                if (value != null)
                    _visitGroups = null;
                _visits = value;
            }
        }

        public PersistantEntity ToPersistantEntity()
        {
            JsonValue = JsonFormatter.ToJson<VisitGroup>(this);
            return this;
        }
        public void LoadFromPersistantEntity()
        {
            var loaded = JsonFormatter.FromJson<VisitGroup>(this.JsonValue);
            if (loaded.VisitGroups != null) this.VisitGroups = loaded.VisitGroups;
            else if (loaded.Visits != null) this.Visits = loaded.Visits;
        }

        public bool AddVisitGroupById(int visitGroupId)
        {
            IRepositoryLocator<VisitGroup> visitGroupLocator = new RepositoryLocator<VisitGroup>();
            IRepository<VisitGroup> repository = visitGroupLocator.GetRepository(RepositoryType.FromJsonRepository, null);
            VisitGroup toBeAdded = repository.GetById(visitGroupId);
            if (VisitGroups == null)
                VisitGroups = new List<VisitGroup>();
            VisitGroups.Add(toBeAdded);
            return true;
        }

        public bool AddVisitById(int visitId)
        {
            IRepositoryLocator<Visit> visitLocator = new RepositoryLocator<Visit>();
            //ClinicalDataContext db = new ClinicalDataContext();
            IRepository<Visit> repository = visitLocator.GetRepository(RepositoryType.FromJsonRepository, null); // db);
            Visit toBeAdded = repository.GetById(visitId);
            if (Visits == null)
                Visits = new List<Visit>();
            Visits.Add(toBeAdded);
            return true;
        }

        public int NumberMyVisitsInternally(int visitInternalId = 0)
        {
            visitInternalId++;

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
