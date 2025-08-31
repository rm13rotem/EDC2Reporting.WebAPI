using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Services
{
    public interface IAuditService
    {
        Task LogAsync(string userId, string action, string entityName, string entityId, 
            string changesJson = null, string metadataJson = null);

    }
}
