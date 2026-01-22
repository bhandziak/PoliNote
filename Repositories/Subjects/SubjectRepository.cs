using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.Models.Subjects;

namespace PoliNote.Repositories.Subjects
{
    public class SubjectRepository(AppDbContext context) : Repository(context)
    {
        // subject details
        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        // fetches subject with their groups
        public async Task<Subject?> GetByIdAsync(Guid id)
        {
            return await _context.Subjects
                .Include(s => s.Groups)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        // add
        public async Task AddAsync(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
        }

        // update
        public async Task UpdateAsync(Subject subject)
        {
            _context.Entry(subject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // delete
        public async Task DeleteAsync(Guid id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                throw new KeyNotFoundException($"Subject with ID {id} was not found.");

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }
}
