using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using PoliNote.Models;

namespace PoliNote.Services;

public class AuthService
{
    // returns encrypted user info (UserId, Username, Role) as indentity session cookie
    public ClaimsPrincipal CreatePrincipal(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        return new ClaimsPrincipal(identity);
    }
}