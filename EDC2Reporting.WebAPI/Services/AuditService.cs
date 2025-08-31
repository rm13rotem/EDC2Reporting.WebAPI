using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models.CrfModels;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Services
{
    public class AuditService : IAuditService
    {
        private readonly EdcDbContext _db;
        private readonly IHttpContextAccessor _http;

        public AuditService(EdcDbContext db, IHttpContextAccessor http)
        {
            _db = db;
            _http = http;
        }

        public async Task LogAsync(string userId, string action, string entityName, string entityId, 
            string changesJson = null, string metadataJson = null)
        {
            var ip = _http.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var ua = _http.HttpContext?.Request?.Headers["User-Agent"].ToString();
            var meta = new Dictionary<string, string>
            {
                ["ip"] = ip,
                ["userAgent"] = ua
            };
            if (!string.IsNullOrWhiteSpace(metadataJson))
            {
                try
                {
                    var extra = JsonSerializer.Deserialize<Dictionary<string, string>>(metadataJson);
                    if (extra != null)
                        foreach (var kv in extra) meta[kv.Key] = kv.Value;
                }
                catch { }
            }

            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                TimestampUtc = DateTime.UtcNow,
                ChangesJson = changesJson,
                MetadataJson = JsonSerializer.Serialize(meta)
            };

            _db.AuditLogs.Add(log);
            await _db.SaveChangesAsync();
        }
    }
}
