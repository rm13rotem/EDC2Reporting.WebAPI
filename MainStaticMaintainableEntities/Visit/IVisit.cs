using DataServices.SqlServerRepository.Models.CrfModels;
using System.Collections.Generic;

namespace MainStaticMaintainableEntities.VisitAssembly
{
    public interface IVisit
    {
        string GuidId { get; set; }
        int Id { get; set; }
        int InternalIndex { get; set; }
        bool IsDeleted { get; set; }
        string JsonValue { get; set; }
        List<CrfPage> CrfPages { get; set; }
        string Name { get; set; }
    }
}