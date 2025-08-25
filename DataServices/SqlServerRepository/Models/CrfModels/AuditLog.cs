﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DataServices.SqlServerRepository.Models.CrfModels
{
    public class AuditLog
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty; // who

        [Required]
        public string Action { get; set; } = string.Empty; // Created/Updated/Viewed/Deleted

        [Required]
        public string EntityName { get; set; } = string.Empty; // CrfEntry, CrfPage

        [Required]
        public string EntityId { get; set; } = string.Empty; // e.g., "42"

        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;

        public string? ChangesJson { get; set; } // optional diff or snapshot
        public string? BeforeJson { get; set; }
        public string AfterJson { get; set; }
        public string? MetadataJson { get; set; } // optional context: IP, PatientId, etc.
    }
}
