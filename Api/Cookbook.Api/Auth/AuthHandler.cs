using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cookbook.Api.Auth;

public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly SessionStore sessionStore;
    
    public AuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        SessionStore sessionStore) : base(options, logger, encoder, clock)
    {
        this.sessionStore = sessionStore;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Cookies.TryGetValue(AuthConsts.SidCookieName, out var sid))
        {
            return AuthenticateResult.Fail("No sid found");
        }

        var userId = sessionStore.Get(sid);
        if (userId == null)
            return AuthenticateResult.Fail("Invalid sid");

        return AuthenticateResult.Success(null);
    }
}