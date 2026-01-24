using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.DTOs.Users;
using PoliNote.Models.Users;
using PoliNote.Repositories.Users;
using PoliNote.Services;
using PoliNote.Services.auth;
using PoliNote.Services.Auth;

namespace PoliNote.Controllers;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly EmailSender _emailSender;
    private readonly AuthService _authService;

    public UsersController(
        UserRepository userRepository,
        EmailSender emailSender,
        AuthService authService)
    {
        _userRepository = userRepository;
        _emailSender = emailSender;
        _authService = authService;
    }

    // GET api/admin/users
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    // POST api/admin/users
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        if (await _userRepository.GetUserByUsernameAsync(dto.Username) != null)
            return BadRequest("Username is already taken");

        if (await _userRepository.GetUserByEmailAsync(dto.Email) != null)
            return BadRequest("Email is already taken");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Role = dto.Role,
            IsActivated = false,
            ActivationToken = Guid.NewGuid().ToString() // activation token
        };

        await _userRepository.CreateAsync(user);

        var activationLink = $"http://localhost:5275/activate?token={user.ActivationToken}";

        await _emailSender.SendEmailAsync(user.Email, "Activate account",
            $"Welcome! Click here to set a password: {activationLink}");

        return Ok(new { message = "User created and email sent." });
    }


    // PATCH api/admin/users/{userId}/role
    [HttpPatch("{userId:guid}/role")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUserRole(Guid userId, [FromBody] ChangeRoleDto dto)
    {
        var recipientUserId = _authService.GetCurrentUserId();
        if (recipientUserId == null) return Unauthorized("User is not logged in");

        if(recipientUserId == userId)
        {
            return BadRequest("You can't change your role");
        }

        var foundUser = await _userRepository.GetUserByIdAsync(userId);

        if (foundUser == null)
            return NotFound($"User with ID {userId} not found.");

        foundUser.Role = dto.NewRole;

        await _userRepository.UpdateAsync(foundUser);

        return Ok(new { message = $"Role updated to {dto.NewRole} for user {foundUser.Username}" });
    }

    // DELETE api/admin/users/{userId}
    [HttpDelete("{userId:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var currentAdminId = _authService.GetCurrentUserId();
        if (currentAdminId == null) return Unauthorized();

        if (currentAdminId == userId)
        {
            return BadRequest("You cannot delete your own admin account.");
        }

        var foundUser = await _userRepository.GetUserByIdAsync(userId);
        if (foundUser == null)
            return NotFound($"User with ID {userId} not found.");

        await _userRepository.DeleteAsync(foundUser);

        return Ok(new { message = $"User {foundUser.Username} has been successfully deleted." });
    }

    // PATCH api/admin/users/{userId}/password
    [HttpPost("{userId:guid}/password")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ResetUserPassword(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
            return NotFound($"User with ID {userId} not found.");

        // reset password
        user.IsActivated = false;
        user.PasswordHash = string.Empty;
        user.ActivationToken = Guid.NewGuid().ToString(); // new token

        await _userRepository.UpdateAsync(user);

        var resetLink = $"http://localhost:5275/activate?token={user.ActivationToken}";

        await _emailSender.SendEmailAsync(user.Email, "Reset Password",
            $"An administrator has reset your password. Click here to set a new one: {resetLink}");

        return Ok(new { message = "Password reset initiated and email sent." });
    }
}