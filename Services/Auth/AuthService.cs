using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using PoliNote.Models;
using System.Security.Claims;

namespace PoliNote.Services.auth;

public class AuthService(IHttpContextAccessor httpContextAccessor)
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

    // get UserId from cookie
    public int? GetCurrentUserId()
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(userIdClaim, out int userId))
        {
            return userId;
        }

        return null;
    }
}