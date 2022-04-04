using System.Threading.Tasks;
using Cookbook.Api.Auth;
using Cookbook.Api.Controllers.Users.Models;
using Cookbook.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.Api.Controllers.Users;

[Route("v1/users")]
public class V1UsersController : ControllerBase
{
    private readonly IUsersRepository usersRepository;
    private readonly SessionStore sessionStore;

    public V1UsersController(IUsersRepository usersRepository, SessionStore sessionStore)
    {
        this.usersRepository = usersRepository;
        this.sessionStore = sessionStore;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        if (!HttpContext.Request.Cookies.TryGetValue(AuthConsts.SidCookieName, out var sid))
        {
            return Unauthorized();
        }
        
        var userId = sessionStore.Get(sid);

        if (userId == null)
            return NotFound();

        var user = await usersRepository.GetByIdAsync(userId.Value).ConfigureAwait(false);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var currentUser = await usersRepository.GetByUsernameAsync(request.Username)
            .ConfigureAwait(false);

        if (currentUser != null)
            return Conflict();

        if (UserFactory.TryCreate(request.Username, request.Password, out var user))
        {
            await usersRepository.SaveUserAsync(user).ConfigureAwait(false);
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await usersRepository.GetByUsernameAsync(request.Username).ConfigureAwait(false);

        if (user == null)
            return NotFound();

        if (user.PasswordHash == PasswordHasher.Hash(request.Password))
        {
            var sid = SidGenerator.GenerateSid();
            sessionStore.Set(sid, user.Id);
            HttpContext.Response.Cookies.Append("cookbook.sid", sid, new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                MaxAge = null
            });

            return Ok(user);
        }

        return BadRequest();
    }
}