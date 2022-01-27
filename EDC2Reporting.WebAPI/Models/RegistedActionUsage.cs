namespace EDC2Reporting.WebAPI.Models
{
    public class RegisteredActionUsage
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int Usage { get; set; }
    }
}