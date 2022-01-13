using DataServices.Providers;
using DataServices.SqlServerRepository.Models;
using MainStaticMaintainableEntities;
using MainStaticMaintainableEntities.ModuleAssembly;
using MainStaticMaintainableEntities.PatientAssembley;
using MainStaticMaintainableEntities.VisitAssembly;
using MainStaticMaintainableEntities.VisitGroup;
using System;

namespace SessionLayer
{
    public class SessionWrapper
    {
        private readonly RepositoryOptions _repositoryOptions;

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
        public ModuleInfo CurrentResult { get; set; }
        public VisitGroup MainVisitGroupOfExperiment
        {
            get
            {
                return null; //from ExperimentId
            }
        }
        public Doctor CurrentDoctor { get; set; }
        public Visit CurrentVisit { get; set; }
        public Patient CurrentPatientDetails { get; set; }
        public Module CurrentModuleInVisitBeingHandled { get; set; }
    }
}
