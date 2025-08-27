using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace EDC2Reporting.WebAPI.Filters
{

    public class LogExceptionsFilter : IExceptionFilter
    {
        private readonly ILogger<LogExceptionsFilter> _logger;

        public LogExceptionsFilter(ILogger<LogExceptionsFilter> logger)
        {
            _logger = logger;    
        }
        public void OnException(ExceptionContext context)
        {
            var user = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
            var action = context.ActionDescriptor.DisplayName;

            _logger.LogError(context.Exception,
                "User {User} triggered an exception in {Action}", user, action);
        }

    }

}
