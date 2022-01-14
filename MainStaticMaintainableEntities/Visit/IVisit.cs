using MainStaticMaintainableEntities.ModuleAssembly;
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
        List<ModuleAssembly.Module> Modules { get; set; }
        string Name { get; set; }
    }
}