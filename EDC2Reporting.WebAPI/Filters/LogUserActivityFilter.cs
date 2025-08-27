
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EDC2Reporting.WebAPI.Filters
{
    public class LogUserActivityFilter : IActionFilter
    {
        private readonly ILogger<LogUserActivityFilter> _logger;

        public LogUserActivityFilter(ILogger<LogUserActivityFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
            var action = context.ActionDescriptor.DisplayName;

            _logger.LogInformation("User {User} is executing {Action}", user, action);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var user = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
            var action = context.ActionDescriptor.DisplayName;

            if (context.Exception == null) // only log success here
            {
                _logger.LogInformation("User {User} successfully executed {Action}", user, action);
            }
        }
    }

}
