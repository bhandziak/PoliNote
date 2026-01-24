using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.DTOs.Users;
using PoliNote.Models.Users;
using PoliNote.Repositories.Users;
using PoliNote.Services;
using PoliNote.Services.Auth;

namespace PoliNote.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly EmailSender _emailSender;

    public UsersController(
        UserRepository userRepository,
        EmailSender emailSender)
    {
        _userRepository = userRepository;
        _emailSender = emailSender;
    }

    // GET api/users
    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    // POST api/users
    [HttpPost("users")]
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

        return Ok(new { Message = "User created and email sent." });
    }
}