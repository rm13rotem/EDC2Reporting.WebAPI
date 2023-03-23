using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SessionLayer
{
    public class SessionService
    {
        public int MaxCached;
        public Dictionary<int, CachedModuleInfo> CachedList;

        private static SessionService _instance;
        public static SessionService Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                _instance = new SessionService();
                return _instance;
            }
        }

        private SessionService()
        {
            // Singelton
            MaxCached = 20000;
            CachedList = new Dictionary<int, CachedModuleInfo>();
        }

        public ModuleInfo GetModuleInfoFrom(int CurrentResultId, int ExperimentId, int CurrentUserId)
        {
            ModuleInfo result;
            if (CachedList.Keys.Contains(CurrentResultId))
            {
                result = CachedList[CurrentResultId].CachedModuleInfoEntity;
                if (result.ExperimentId == ExperimentId)
                    return result; // Safety check;
                else return null;
            }

            using EdcDbContext db = new EdcDbContext();
            if (CurrentResultId > 0)
            {
                result = db.ModuleInfos.
                    First(x => x.Id == CurrentResultId && x.ExperimentId == ExperimentId);
                var cachedResult = new CachedModuleInfo()
                {
                    FirstUsed = DateTime.UtcNow,
                    CachedModuleInfoEntity = result
                };
                CachedList[CurrentResultId] = cachedResult;

                PurgeOneDayOldFromCache();
                return result;
            }
            else
            {
                result = new ModuleInfo()
                {
                    ExperimentId = ExperimentId,
                    CreatedDateTime = DateTime.UtcNow,
                    CurrentLastUpdateDateTime = DateTime.UtcNow,
                    LastUpdatedDateTime = DateTime.UtcNow,
                    LastUpdator = CurrentUserId,

                };
                db.ModuleInfos.Add(result);
                db.SaveChanges();
                CurrentResultId = result.Id; // new Id;
                return result; //And work on it and save it.
            }
        }

        private void PurgeOneDayOldFromCache()
        {
            var toBeDeletedKeys = CachedList.Where(x => x.Value.FirstUsed < DateTime.UtcNow.AddDays(-1))
                .Select(x => x.Key).ToList();

            foreach (var old_key in toBeDeletedKeys)
            {
                CachedList.Remove(old_key);
            }
        }
    }
}