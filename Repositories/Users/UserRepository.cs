using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.DTOs.Users;
using PoliNote.Models.Users;

namespace PoliNote.Repositories.Users;

public class UserRepository(AppDbContext context) : Repository(context)
{
    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        return await _context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Role = u.Role
            })
            .ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(
            u => u.Id == id
        );
    }
    public async Task<User?> GetByActivationTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(
            u => u.ActivationToken == token
        );
    }
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(
            u => u.Username == username
        );
    }
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(
            u => u.Email == email
        );
    }

    // create
    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
    // update
    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }


    // delete
    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
