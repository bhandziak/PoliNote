using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PoliNote.DTOs.Auth;
using PoliNote.DTOs.Users;
using PoliNote.Models;
using PoliNote.Repositories;
using PoliNote.Services.auth;

namespace PoliNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly AuthService _authService;

        public AuthController(UserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        // GET api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // find user
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);

            string requestPasswordHash = request.Password;

            if (user == null
                || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid login or password");
            }

            // generate principal
            var principal = _authService.CreatePrincipal(user);

            // create cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = true }
            );

            var userResponse = new UserDto
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };

            return Ok(new { 
                message = "Logged in successfully",
                user = userResponse
            });
        }
    }
}