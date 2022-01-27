using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using MainStaticMaintainableEntities;
using MainStaticMaintainableEntities.ModuleAssembly;
using MainStaticMaintainableEntities.PatientAssembley;
using MainStaticMaintainableEntities.VisitAssembly;
using MainStaticMaintainableEntities.VisitGroup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace SessionLayer
{
    public class SessionWrapper : ISessionWrapper
    {
        private readonly RepositoryOptions _repositoryOptions;
        private readonly string CurrentResultKey = "CurrentResultKey";
        private IHttpContextAccessor _httpContextAccessor;

        public SessionWrapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public SessionWrapper(RepositoryOptions repositoryOptions)
        {
            _repositoryOptions = repositoryOptions;
        }
        // Internal Session Data - alter only in LoginController
        // (these are internal to CurrentResult;

        //public int ExperimentId { get; set; }
        //public int DoctorId { get; set; }
        //public int PatientId { get; set; }
        //public int VisitIndex { get; set; }
        //public int CurrentModuleId { get; set; }

        // Data Collected in ModuleDisplayController - 1 action per CRF page / Module
        public int CurrentResultId
        {
            get
            {
                
                var httpContext = _httpContextAccessor.HttpContext;
                byte[] byteArray;
                if (httpContext.Session.TryGetValue(CurrentResultKey, out byteArray))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    MemoryStream stream = new MemoryStream(byteArray);
                    int result = (int) formatter.Deserialize(stream);
                    return result;
                }
                return 0;
            }

            set
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream memStream = new MemoryStream();
                object o = value;
                formatter.Serialize(memStream, o);
                _httpContextAccessor.HttpContext.Session.Set(CurrentResultKey, memStream.ToArray());
            }
        }

        public ModuleInfo CurrentResult
        {

            get
            {
                ModuleInfo result;

                using (EdcDbContext db = new EdcDbContext())
                {
                    if (CurrentResultId > 0)
                    {
                        result = db.ModuleInfos.FirstOrDefault(x => x.Id == CurrentResultId);
                        result = new ModuleInfo();
                        return result;
                    }
                    else
                    {
                        result = new ModuleInfo();
                        db.ModuleInfos.Add(result);
                        db.SaveChanges();
                        CurrentResultId = result.Id; // new Id;
                        return result; //And work on it and save it.
                    }
                }
            }
        }
        public Experiment CurrentExperiment
        {
            get
            {
                using (EdcDbContext db = new EdcDbContext())
                {
                    var result = db.Experiments.FirstOrDefault(x => x.Id == CurrentResult.ExperimentId);
                    return result;
                }
            }
        }

        public VisitGroup MainVisitGroupOfExperiment
        {
            get
            {
                return GetPersistentEntity<VisitGroup>();
            }
        }

        public Doctor CurrentDoctor
        {
            get
            {
                return GetPersistentEntity<Doctor>();
            }
        }

        private T GetPersistentEntity<T>()
            where T : IPersistentEntity, new()
        {
            if (CurrentResult == null)
                return default(T); // TODO : throw exception? Log?

            var locator = new RepositoryLocator<T>();
            var repo = locator.GetRepository(_repositoryOptions.RepositoryType);

            return repo.GetById(GetId(typeof(T).Name)); //from Session
        }


        public Visit CurrentVisit
        {
            get { return GetPersistentEntity<Visit>(); }
        }
        public Patient CurrentPatientDetails
        {
            get { return GetPersistentEntity<Patient>(); }
        }
        public Module CurrentModuleInVisitBeingHandled
        {
            get { return GetPersistentEntity<Module>(); }
        }
        private int GetId(string typeName)
        {
            typeName = typeName.Split('.').Last(); // e.g. AssemblySite.Site --> Site

            if (typeName == "Doctor")
                return CurrentResult.DoctorId;
            if (typeName == "Patient")
                return CurrentResult.PatientId;
            if (typeName == "Module")
                return CurrentResult.ModuleId;
            if (typeName == "Visit")
                return CurrentResult.VisitId;
            if (typeName == "VisitGroupId")
                return CurrentResult.VisitGroupId;
            return 1; // TODO : Log? throw Exception?
        }

    }
}
