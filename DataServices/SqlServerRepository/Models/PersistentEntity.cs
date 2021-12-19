using DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataServices.SqlServerRepository.Models
{
    public partial class PersistentEntity : IPersistentEntity, IHasJsonValue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string GuidId { get; set; }

        [Required]
        [StringLength(100)]
        public string EntityName { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }


        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string JsonValue { get; set; }

        public bool IsDeleted { get; set; }
    }
}
