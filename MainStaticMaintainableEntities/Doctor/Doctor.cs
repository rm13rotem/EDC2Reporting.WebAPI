using System;

namespace MainStaticMaintainableEntities
{
    public abstract class Doctor : PersistantEntity
    {
        public DateTime DateOfBirth { get; set; }
        public int DoctorNumber { get; set; }

    }
}
