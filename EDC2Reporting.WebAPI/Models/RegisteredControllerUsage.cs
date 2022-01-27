using System.Collections.Generic;
using System.Linq;

namespace EDC2Reporting.WebAPI.Models
{
    public class RegisteredControllerUsage
    {
        public RegisteredControllerUsage()
        {
            ActionUsage = new List<RegisteredActionUsage>();
            ControllerUsage = new Dictionary<string, int>();
        }
        public List<RegisteredActionUsage> ActionUsage { get; private set; }
        public Dictionary<string, int> ControllerUsage { get; private set; }

        public void LogUsage(string controllerName)
        {
            if (ControllerUsage.Keys.Contains(controllerName))
                ControllerUsage[controllerName]++;
        }

        public void LogUsage(string controllerName, string actionName)
        {
            var line = ActionUsage.FirstOrDefault(x => x.ControllerName == controllerName &&
            x.ActionName == actionName);
            if (line != null)
                line.Usage++;
            else ActionUsage.Add(new RegisteredActionUsage()
            {
                ControllerName = controllerName,
                ActionName = actionName,
                Usage = 1
            }); ;
        }
    }
}