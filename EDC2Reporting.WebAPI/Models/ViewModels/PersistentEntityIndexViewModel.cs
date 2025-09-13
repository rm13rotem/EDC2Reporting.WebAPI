using DataServices.SqlServerRepository.Models;
using System.Collections.Generic;

namespace EDC2Reporting.WebAPI.Models.ViewModels
{
    public class PersistentEntityIndexViewModel
    {
        public PersistentEntityFilter Filter { get; internal set; }
        public List<PersistentEntity> Results { get; internal set; }
    }
}
