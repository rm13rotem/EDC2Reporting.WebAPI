using MainStaticMaintainableEntities;
using MainStaticMaintainableEntities.SiteAssembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models.RegisterModels
{
    public class RegisterViewModel : QuickLookIdSetupRegister, IInvestigator
    {
        public DateTime DateOfBirth { get; set; }
        public int DoctorNumber { get; set; }
        public string GuidId { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string JsonValue { get; set; }
        public string Name { get; set; }
        public Site Site { get; set; }
        public int SiteId { get; set; }
    }
}
