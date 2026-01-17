using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PoliNote.Repositories
{
    public class PublicCalendarRepository(AppDbContext context) : Repository(context)
    {
        public async Task<IEnumerable<PublicEvent>> GetByDateAsync(DateTime date)
        {
            return await _context.PublicEvents
                .Where(e => e.Date.Date == date.Date)
                .OrderBy(e => e.StartTime)
                .ToListAsync();
        }

        public async Task<PublicEvent?> GetByIdAsync(Guid id)
        {
            return await _context.PublicEvents
                .Include(e => e.CreatedByUser)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task PutAsync(PublicEvent publicEvent)
        {
            await _context.PublicEvents.AddAsync(publicEvent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PublicEvent publicEvent)
        {
            _context.Entry(publicEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var publicEvent = await _context.PublicEvents.FindAsync(id);

            if (publicEvent == null)
                throw new KeyNotFoundException($"Event with ID {id} was not found.");


            _context.PublicEvents.Remove(publicEvent);
            await _context.SaveChangesAsync();
        }
    }
}
