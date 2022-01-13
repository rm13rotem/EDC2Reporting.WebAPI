using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using MainStaticMaintainableEntities.ModuleAssembly;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MainStaticMaintainableEntities.VisitAssembly
{
    public class Visit : IPersistentEntity, IVisit
    {
        [JsonIgnore]
        public List<Module> Modules { get; set; }
        [JsonIgnore]
        public int InternalIndex { get; set; }

        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string JsonValue { get; set; }
        public string Name { get; set; }
        public DbAccessOptions DbAccessOptions { get; private set; }

        public Visit()
        {

        }
        public Visit(IOptionsSnapshot<DbAccessOptions> snapshotOptions)
        {
            DbAccessOptions = snapshotOptions.Value;
        }
        public Visit(PersistentEntity details, List<int> moduleIds = null)
        {
            Id = details.Id;
            Name = details.Name;
            IsDeleted = details.IsDeleted;
            GuidId = details.GuidId;

            if (moduleIds != null)
            {
                JsonValue = JsonConvert.SerializeObject(moduleIds);
            }
            else moduleIds = JsonConvert.DeserializeObject<List<int>>(details.JsonValue);

            if (moduleIds == null)
                return; // almost - throw exception;

            IRepositoryLocator<Module> repositoryLocator = new RepositoryLocator<Module>();
            var module_repository = repositoryLocator.GetRepository(RepositoryType.FromDbRepository);
            Modules = new List<Module>();
            foreach (int ModuleId in moduleIds)
            {
                var m = module_repository.GetById(ModuleId);
                if (m != null)
                    Modules.Add(m);
            }
        }

        private async Task<bool> TryUpdateJsonValuePropertiesAsync(PersistentEntity details, List<int> moduleIds)
        {
            try
            {
                using (EdcDbContext db = new EdcDbContext())
                {
                    if (string.IsNullOrWhiteSpace(details.JsonValue))
                    {
                        db.PersistentEntities.Attach(details);
                        details.JsonValue = JsonValue;
                        db.Entry<PersistentEntity>(details).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //logger ex
                for (int i = 0; i < DbAccessOptions.DbNRetrys; i++)
                {
                    try
                    {
                        using (EdcDbContext db = new EdcDbContext())
                        {
                            if (string.IsNullOrWhiteSpace(details.JsonValue))
                            {
                                db.PersistentEntities.Attach(details);
                                details.JsonValue = JsonValue;
                                db.Entry<PersistentEntity>(details).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        return true;
                    }
                    catch
                    {
                        await Task.Delay(DbAccessOptions.DbWaitTimeInSeconds * 1000);
                    }
                }
                return false;
            }
        }
    }
}