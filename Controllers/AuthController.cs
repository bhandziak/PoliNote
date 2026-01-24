using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PoliNote.DTOs.Auth;
using PoliNote.DTOs.Users;
using PoliNote.Models;
using PoliNote.Models.Users;
using PoliNote.Repositories.Users;
using PoliNote.Services;
using PoliNote.Services.auth;

namespace PoliNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly AuthService _authService;
        private readonly IDataValidator<ActivateRequestDto> _activationValidator;
        public AuthController(
            UserRepository userRepository,
            AuthService authService,
            IDataValidator<ActivateRequestDto> activationValidator)
        {
            _userRepository = userRepository;
            _authService = authService;
            _activationValidator = activationValidator;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // find user
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);

            if (user == null
                || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid login or password");
            }

            // pass only activated users
            if(!user.IsActivated)
                return Unauthorized("User is not activated");

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
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                Role = user.Role
            };

            return Ok(new { 
                message = "Logged in successfully",
                user = userResponse
            });
        }

        // POST api/auth/logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = _authService.GetCurrentUserId();
            if (userId == null)
                return BadRequest("User is already logged out");

            // delete cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new { message = "Logged out successfully" });
        }

        // POST api/auth/activate
        [HttpPost("activate")]
        public async Task<IActionResult> Activate([FromBody] ActivateRequestDto request)
        {
            var user = await _userRepository.GetByActivationTokenAsync(request.Token);

            if (user == null)
                return BadRequest("Invalid or expired token.");

            _activationValidator.ValidateOrThrow(request);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.IsActivated = true;
            user.ActivationToken = string.Empty;

            await _userRepository.UpdateAsync(user);

            return Ok(new { Message = "Your password has been set. You can log in." });
        }
    }
}