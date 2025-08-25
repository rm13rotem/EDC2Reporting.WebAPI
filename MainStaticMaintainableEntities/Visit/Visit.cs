using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using DataServices.SqlServerRepository.Models.CrfModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainStaticMaintainableEntities.VisitAssembly
{
    public class Visit : IPersistentEntity, IVisit
    {
        [JsonIgnore]
        public List<CrfPage> CrfPages { get; set; }
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
        public Visit(PersistentEntity details, List<int> crfPageIds = null)
        {
            Id = details.Id;
            Name = details.Name;
            IsDeleted = details.IsDeleted;
            GuidId = details.GuidId;

            if (crfPageIds != null)
            {
                JsonValue = JsonConvert.SerializeObject(crfPageIds);
            }
            else crfPageIds = JsonConvert.DeserializeObject<List<int>>(details.JsonValue);

            if (crfPageIds == null)
                return; // almost - throw exception;

            //IRepositoryLocator<CrfPage> repositoryLocator = new RepositoryLocator<CrfPage>();
            //var module_repository = repositoryLocator.GetRepository(RepositoryType.FromDbRepository);
            var db = new EdcDbContext();
            CrfPages = db.CrfPages.ToList();
            foreach (int CrfPageId in crfPageIds)
            {
                var m = CrfPages.FirstOrDefault(x=> x.Id == CrfPageId);
                if (m != null)
                    db.CrfPages.Add(m);
            }
        }

        //private async Task<bool> TryUpdateJsonValuePropertiesAsync(PersistentEntity details, List<int> moduleIds)
        //{
        //    try
        //    {
        //        using (EdcDbContext db = new EdcDbContext())
        //        {
        //            if (string.IsNullOrWhiteSpace(details.JsonValue))
        //            {
        //                db.PersistentEntities.Attach(details);
        //                details.JsonValue = JsonValue;
        //                db.Entry<PersistentEntity>(details).State = EntityState.Modified;
        //                db.SaveChanges();
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logger ex
        //        for (int i = 0; i < DbAccessOptions.DbNRetrys; i++)
        //        {
        //            try
        //            {
        //                using (EdcDbContext db = new EdcDbContext())
        //                {
        //                    if (string.IsNullOrWhiteSpace(details.JsonValue))
        //                    {
        //                        db.PersistentEntities.Attach(details);
        //                        details.JsonValue = JsonValue;
        //                        db.Entry<PersistentEntity>(details).State = EntityState.Modified;
        //                        db.SaveChanges();
        //                    }
        //                }
        //                return true;
        //            }
        //            catch
        //            {
        //                await Task.Delay(DbAccessOptions.DbWaitTimeInSeconds * 1000);
        //            }
        //        }
        //        return false;
        //    }
        //}
    }
}