using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cookbook.Api.Auth;

public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly ITicketStore ticketStore;
    private readonly SessionStore sessionStore;
    
    public AuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        ITicketStore ticketStore,
        SessionStore sessionStore) : base(options, logger, encoder, clock)
    {
        this.ticketStore = ticketStore;
        this.sessionStore = sessionStore;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        
        var ticket = await ticketStore.RetrieveAsync(Context.Session.Id).ConfigureAwait(false);
        if (ticket == null)
            return AuthenticateResult.Fail($"No ticket for sessions {Context.Session.Id}");
        
        return AuthenticateResult.Success(ticket);
    }
}