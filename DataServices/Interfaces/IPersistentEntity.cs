namespace DataServices.Interfaces
{
    public interface IPersistentEntity
    {
        int Id { get; set; }
        string GuidId { get; set; }
        bool IsDeleted { get; set; }
        string JsonValue { get; set; }
        string Name { get; set; }
    }
}