using System;

namespace DataServices.SqlServerRepository.Models
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
        int SiteId { get; set; }
    }
}