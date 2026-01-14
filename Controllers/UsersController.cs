using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.Models;

namespace PoliNote.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }
}
