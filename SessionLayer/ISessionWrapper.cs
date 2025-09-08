
namespace SessionLayer
{
    public interface ISessionWrapper
    {
        int SelectedPatientId { get; set; }
        int SelectedVisitGroupId { get; set; }
        int SelectedVisitIndex { get; set; }
        int SelectedVisitId { get; set; }
        int SelectedCrfId { get; set; }
    }
}