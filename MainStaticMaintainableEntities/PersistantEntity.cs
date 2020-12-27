using MainStaticMaintainableEntities.Interfaces;
using System;

namespace MainStaticMaintainableEntities
{
    public abstract class PersistantEntity : IPersistantEntity
    {
        public virtual int Id { get; set; }
        public virtual string GuidId { get; set; }
        public virtual string EntityName { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual string Name { get; set; }
        public virtual string JsonValue { get; set; }
        public bool IsDeleted { get; set; }
    }
}