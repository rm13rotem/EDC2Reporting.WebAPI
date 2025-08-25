using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository.Models;
using DataServices.SqlServerRepository.Models.CrfModels;
using MainStaticMaintainableEntities.PatientAssembley;
using MainStaticMaintainableEntities.VisitAssembly;
using MainStaticMaintainableEntities.VisitGroupAssembley;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace SessionLayer
{
    public class SessionWrapper : ISessionWrapper
    {
        private readonly RepositoryOptions _repositoryOptions;
        private readonly ISession _session;

        private readonly string CurrentResultKey = "CurrentResultKey";
        private readonly string CurrentLoginKey = "CurrentLoginKey";
        private readonly string CurrentSessionStartDateTime = "CurrentSessionStartDateTime";

        public SessionWrapper(IHttpContextAccessor httpContextAccessor,
            IOptionsSnapshot<RepositoryOptions> options)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _repositoryOptions = options.Value;
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
                int? v = _session.GetInt32(CurrentResultKey);
                if (v.HasValue)
                    return v.Value;
                else
                    return 0;
            }

            set { _session.SetInt32(CurrentResultKey, value); }
        }
        public int CurrentLoginId
        {
            get
            {
                int? v = _session.GetInt32(CurrentLoginKey);
                if (v.HasValue)
                    return v.Value;
                else
                    return 0;
            }

            set { _session.SetInt32(CurrentLoginKey, value); }
        }
        public DateTime StartDateTime
        {
            get
            {
                var result = _session.GetString(CurrentSessionStartDateTime);
                if (string.IsNullOrWhiteSpace(result))
                {
                    StartDateTime = DateTime.UtcNow;
                    return DateTime.UtcNow;
                }
                return DateTime.Parse(result);
            }

            set
            {
                _session.SetString(CurrentSessionStartDateTime,
                value.ToUniversalTime().ToString("ddMMMyyyy HH:mm"));
            }
        }

        //private void SetValue<T>(string key, T value)
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    MemoryStream memStream = new MemoryStream();
        //    object o = value;
        //    formatter.Serialize(memStream, o);
        //    _session.Set(key, memStream.ToArray());
        //}

        //private T GetValue<T>(string key)
        //{
        //    var httpContext = _httpContextAccessor.HttpContext;
        //    byte[] byteArray;
        //    if (httpContext.Session.TryGetValue(key, out byteArray))
        //    {
        //        BinaryFormatter formatter = new BinaryFormatter();
        //        MemoryStream stream = new MemoryStream(byteArray);
        //        T result = (T)formatter.Deserialize(stream);
        //        return result;
        //    }
        //    return default(T);
        //}

        public ModuleInfo CurrentResult
        {

            get
            {
                SessionService SessionServiceInstance = SessionService.Instance;
                ModuleInfo result =
                    SessionServiceInstance.GetModuleInfoFrom(CurrentResultId, CurrentUser.ExperimentId, CurrentLoginId);
                return result;
            }
        }

        private T GetPersistentEntity<T>()
            where T : IPersistentEntity, new()
        {
            if (CurrentResult == null)
                return default; // TODO : throw exception? Log?

            var locator = new RepositoryLocator<T>();
            var repo = locator.GetRepository(_repositoryOptions.RepositoryType);

            return repo.GetById(GetId(typeof(T).Name)); //from Session
        }


        public VisitGroup MainVisitGroupOfExperiment
        {
            get
            {
                return GetPersistentEntity<VisitGroup>();
            }
        }

        public Investigator CurrentInvestigator
        {
            get
            {
                return null; // GetPersistentEntity<Investigator>();
            }
        }

        
        public Visit CurrentVisit
        {
            get { return GetPersistentEntity<Visit>(); }
        }
        public Patient CurrentPatientDetails
        {
            get { return GetPersistentEntity<Patient>(); }
        }
        public CrfPage CurrentCrfPageInVisitBeingHandled
        {
            get { return GetPersistentEntity<CrfPage>(); }
        }

        public EdcUser CurrentUser { get; set; } // string?

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
