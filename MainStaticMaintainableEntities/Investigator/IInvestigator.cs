﻿using MainStaticMaintainableEntities.SiteAssembly;
using System;

namespace MainStaticMaintainableEntities
{
    public interface IInvestigator
    {
        DateTime DateOfBirth { get; set; }
        int DoctorNumber { get; set; }
        string GuidId { get; set; }
        int Id { get; set; }
        bool IsDeleted { get; set; }
        string JsonValue { get; set; }
        string Name { get; set; }
        Site Site { get; set; }
        int SiteId { get; set; }
    }
}