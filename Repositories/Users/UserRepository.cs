using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.Models.Users;

namespace PoliNote.Repositories.Users;

public class UserRepository(AppDbContext context) : Repository(context)
{
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(
            u => u.Id == id
        );
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(
            u => u.Username == username
        );
    }
}
