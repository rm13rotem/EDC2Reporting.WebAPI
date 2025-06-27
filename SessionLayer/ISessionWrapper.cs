using DataServices.SqlServerRepository.Models;
using MainStaticMaintainableEntities;
using MainStaticMaintainableEntities.ModuleAssembly;
using MainStaticMaintainableEntities.PatientAssembley;
using MainStaticMaintainableEntities.VisitAssembly;
using MainStaticMaintainableEntities.VisitGroupAssembley;

namespace SessionLayer
{
    public interface ISessionWrapper
    {
        Doctor CurrentDoctor { get; }
        Module CurrentModuleInVisitBeingHandled { get; }
        Patient CurrentPatientDetails { get; }
        ModuleInfo CurrentResult { get; }
        int CurrentResultId { get; set; }
        Visit CurrentVisit { get; }
        VisitGroup MainVisitGroupOfExperiment { get; }
        EdcUser CurrentUser { get; set; }
    }
}