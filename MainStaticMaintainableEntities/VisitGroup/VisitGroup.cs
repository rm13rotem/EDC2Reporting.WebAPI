using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using MainStaticMaintainableEntities.Interfaces;
using MainStaticMaintainableEntities.VisitAssembly;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace MainStaticMaintainableEntities.VisitGroup
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

        private readonly RepositoryOptions repositoryOption;

        private readonly EdcDbContext dbContext;

        public VisitGroup()
        {
            VisitGroups = new List<VisitGroup>();
            Visits = new List<Visit>();
        }

        public VisitGroup(IOptionsSnapshot<RepositoryOptions> options, EdcDbContext _dbContext)
        {
            repositoryOption = options.Value;
            dbContext = _dbContext;
        }
          
        public void LoadFromDbByIds(List<int> VisitIds, List<int> VisitGroupIds)
        {
            if (VisitIds != null && VisitIds.Count > 0)
            {
                RepositoryLocator<Visit> repositoryLocator = new RepositoryLocator<Visit>();
                var repository = repositoryLocator.GetRepository(repositoryOption.RepositoryType);
                foreach (var visitId in VisitIds)
                {
                    var visit = repository.GetById(visitId);
                    if (visit != null)
                        Visits.Add(visit);
                }
            }
            if (VisitGroupIds != null && VisitGroupIds.Count > 0)
            {

                RepositoryLocator<VisitGroup> repositoryLocator = new RepositoryLocator<VisitGroup>();
                var repository = repositoryLocator.GetRepository(repositoryOption.RepositoryType);
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
