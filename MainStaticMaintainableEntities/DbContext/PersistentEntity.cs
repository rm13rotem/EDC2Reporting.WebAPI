using System;
using System.Collections.Generic;

namespace MainStaticMaintainableEntities
{
    public partial class PersistentEntity
    {
        public int Id { get; set; }
        public string GuidId { get; set; }
        public string EntityName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string JsonValue { get; set; }
        public bool IsDeleted { get; set; }
    }
}
