using System;

namespace MainStaticMaintainableEntities
{
    public class Doctor : PersistantEntity
    {
        public DateTime DateOfBirth { get; set; }
        public int DoctorNumber { get; set; }

    }
}
