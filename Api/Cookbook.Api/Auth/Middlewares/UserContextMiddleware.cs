using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Cookbook.Api.Auth.Middlewares;

public class UserContextMiddleware : IMiddleware
{
    private readonly SessionStore sessionStore;

    public UserContextMiddleware(SessionStore sessionStore)
    {
        this.sessionStore = sessionStore;
    }

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var userContextAttribute = context.Features.Get<IEndpointFeature>()?.Endpoint?
            .Metadata.GetMetadata<UserContextAttribute>();
        
        if (userContextAttribute != null && context.Request.Cookies.TryGetValue(AuthConsts.SidCookieName, out var sid))
        {
            var userId = sessionStore.Get(sid);

            if (userId != null)
                context.Items[AuthConsts.UserIdItemName] = userId.Value;
        }

        return next(context);
    }
}