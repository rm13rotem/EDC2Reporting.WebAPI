using DataServices.SqlServerRepository.Models;
using System;

namespace SessionLayer
{
    public class CachedModuleInfo
    {
        public DateTime FirstUsed { get; set; }
        public ModuleInfo CachedModuleInfoEntity { get; set; }
    }
}