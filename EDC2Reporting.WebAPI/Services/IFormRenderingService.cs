using DataServices.SqlServerRepository.Models.CrfModels;

namespace EDC2Reporting.WebAPI.Services
{
    public interface IFormRenderingService
    { 
        // Returns HTML ready to inject into a Razor view and hydrated with values
        string Render(CrfPage page, string? formDataJson);
    }
}
