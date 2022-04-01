using System.Threading.Tasks;
using Cookbook.Api.Auth;
using Cookbook.Api.Controllers.Users.Models;
using Cookbook.Users;
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
        var userId = sessionStore.Get(HttpContext.Session.Id);

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
            sessionStore.Set(HttpContext.Session.Id, user.Id);
            HttpContext.Session.Set("userId", user.Id.ToByteArray());

            return Ok(user);
        }

        return BadRequest();
    }
}