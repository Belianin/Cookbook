using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Cookbook.Api.Auth.Middlewares;

public static class UserContextMiddlewareExtensions
{
    public static IServiceCollection AddUserContext(this IServiceCollection services)
    {
        return services.AddSingleton<UserContextMiddleware>();
    }

    public static IApplicationBuilder UseUserContext(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UserContextMiddleware>();
    }
}