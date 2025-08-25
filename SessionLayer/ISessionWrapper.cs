using DataServices.SqlServerRepository.Models;
using DataServices.SqlServerRepository.Models.CrfModels;
using MainStaticMaintainableEntities.PatientAssembley;
using MainStaticMaintainableEntities.VisitAssembly;
using MainStaticMaintainableEntities.VisitGroupAssembley;

namespace SessionLayer
{
    public interface ISessionWrapper
    {
        Investigator CurrentInvestigator { get; }
        CrfPage CurrentCrfPageInVisitBeingHandled { get; }
        Patient CurrentPatientDetails { get; }
        ModuleInfo CurrentResult { get; }
        int CurrentResultId { get; set; }
        Visit CurrentVisit { get; }
        VisitGroup MainVisitGroupOfExperiment { get; }
        EdcUser CurrentUser { get; set; }
    }
}