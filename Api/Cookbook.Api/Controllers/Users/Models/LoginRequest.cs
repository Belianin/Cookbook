using System.ComponentModel.DataAnnotations;

namespace Cookbook.Api.Controllers.Users.Models;

public class LoginRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Username { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }
}