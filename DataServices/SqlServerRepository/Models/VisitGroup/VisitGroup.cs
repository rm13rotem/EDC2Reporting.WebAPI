using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository.Models.VisitAssembley;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Utilities;

namespace DataServices.SqlServerRepository.Models.VisitGroup
{
    public class VisitGroup : IPersistentEntity, ILoadableByVisityGroupIds
    {
        public List<VisitGroup> VisitGroups { get; set; }
        public List<Visit> Visits { get; set; }

        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string JsonValue { get; set; }
        public string Name { get; set; }

        
        public VisitGroup()
        {
            VisitGroups = new List<VisitGroup>();
            Visits = new List<Visit>();
        }
         
        public void LoadFromDbByIds(List<int> VisitIds, List<int> VisitGroupIds)
        {
            if (VisitIds != null && VisitIds.Count > 0)
            {
                RepositoryLocator<Visit> repositoryLocator = RepositoryLocator<Visit>.Instance;
                var repository = repositoryLocator.GetRepository(RepositoryType.FromDbRepository);
                foreach (var visitId in VisitIds)
                {
                    var visit = repository.GetById(visitId);
                    if (visit != null)
                        Visits.Add(visit);
                }
            }
            if (VisitGroupIds != null && VisitGroupIds.Count > 0)
            {

                RepositoryLocator<VisitGroup> repositoryLocator = RepositoryLocator<VisitGroup>.Instance;
                var repository = repositoryLocator.GetRepository(RepositoryType.FromDbRepository);
                foreach (var _visitGroupId in VisitGroupIds)
                {
                    var visitGroup = repository.GetById(_visitGroupId);
                    if (visitGroup != null)
                        VisitGroups.Add(visitGroup);
                }
            }
        }

        public void LoadFromPersistentEntity(PersistentEntity persistentEntity)
        {
            var loaded = JsonFormatter.FromJson<VisitGroup>(persistentEntity.JsonValue);
            if (loaded.VisitGroups != null)
            {
                this.VisitGroups = loaded.VisitGroups;
            }
            else if (loaded.Visits != null)
            {
                this.Visits = loaded.Visits;
            }
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
