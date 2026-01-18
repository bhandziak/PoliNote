using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.Models;

namespace PoliNote.Repositories
{
    public class PrivateCalendarRepository(AppDbContext context) : Repository(context)
    {
        public async Task<IEnumerable<PrivateEvent>> GetByDateAsync(DateTime date, int userId)
        {
            return await _context.PrivateEvents
                .Where(e => e.Date.Date == date.Date && e.CreatedByUserId == userId)
                .OrderBy(e => e.Date)
                .ToListAsync();
        }
        public async Task<PrivateEvent?> GetByIdAsync(Guid id)
        {
            return await _context.PrivateEvents
                .Include(e => e.CreatedByUser)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task PutAsync(PrivateEvent privateEvent)
        {
            await _context.PrivateEvents.AddAsync(privateEvent);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(PrivateEvent privateEvent)
        {
            _context.Entry(privateEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var privateEvent = await _context.PrivateEvents.FindAsync(id);

            if (privateEvent == null)
                throw new KeyNotFoundException($"Event with ID {id} was not found.");


            _context.PrivateEvents.Remove(privateEvent);
            await _context.SaveChangesAsync();
        }
    }
}
