using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CourierAppBackend.Auth;

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;
    public ApiKeyAuthFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("XApiKey", out var key))
        {
            context.Result = new UnauthorizedObjectResult("API Key missing");
            return;
        }
        var apiKey = _configuration["Auth:ApiKey"];
        if(!(apiKey == key))
        {
            context.Result = new UnauthorizedObjectResult("Invalid API Key");
            return;
        }
    }
}
