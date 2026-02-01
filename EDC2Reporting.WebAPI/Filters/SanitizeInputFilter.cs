using DataServices.Common.Attributes;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;

namespace EDC2Reporting.WebAPI.Filters
{

    public class SanitizeInputFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null)
                    continue;

                var type = argument.GetType();

                // Check attribute on model OR action
                var hasAttribute =
                    type.GetCustomAttribute<SanitizeInputAttribute>() != null ||
                    context.ActionDescriptor.EndpointMetadata.OfType<SanitizeInputAttribute>().Any();

                if (!hasAttribute)
                    continue;

                SanitizeObject(argument);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        private void SanitizeObject(object obj)
        {
            if (obj == null)
                return;

            var properties = obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var prop in properties)
            {

                if (prop.Name == "Html")
                    continue;
                if (prop.Name == "Item")
                    continue;
                
                if (prop.PropertyType == typeof(string))
                {
                    var value = prop.GetValue(obj) as string;
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        // Example: HTML encode (XSS protection)
                        var sanitized = HtmlEncoder.Default.Encode(value.Trim());
                        prop.SetValue(obj, sanitized);
                    }
                }
                else if (!prop.PropertyType.IsPrimitive &&
                         !prop.PropertyType.IsEnum &&
                         prop.PropertyType != typeof(DateTime))
                {
                    // Recurse into complex objects
                    var nestedObj = prop.GetValue(obj);
                    if (nestedObj != null)
                    {
                        SanitizeObject(nestedObj);
                    }
                }
            }
        }
    }
}