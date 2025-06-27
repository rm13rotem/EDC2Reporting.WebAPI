using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

public class BasicAuthenticationAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authHeader = context.HttpContext.Request.Headers["Authorization"];

        if (string.IsNullOrWhiteSpace(authHeader))
        {
            context.HttpContext.Response.StatusCode = 401;
            await context.HttpContext.Response.WriteAsync("Authorization header missing");
            return;
        }

        // Basic authentication parsing
        var encodedCredentials = authHeader.ToString().Replace("Basic ", "", StringComparison.OrdinalIgnoreCase);
        var decodedBytes = Convert.FromBase64String(encodedCredentials);
        var decodedCredentials = Encoding.UTF8.GetString(decodedBytes);
        var parts = decodedCredentials.Split(':');

        var username = parts[0];
        var password = parts[1];

        // Replace with your real user validation
        if (username != "admin" || password != "password")
        {
            context.HttpContext.Response.StatusCode = 401;
            await context.HttpContext.Response.WriteAsync("Invalid username or password");
            return;
        }

        // If you want to set claims, do it here
    }
}