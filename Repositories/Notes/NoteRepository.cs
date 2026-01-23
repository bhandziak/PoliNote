using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.DTOs.Notes;
using PoliNote.Models.Notes;

namespace PoliNote.Repositories.Notes
{
    public class NoteRepository(AppDbContext context) : Repository(context)
    {
        // get note by userId, subjectGroupId, date
        public async Task<Note?> GetNoteAsync(Guid userId, Guid subjectGroupId, DateOnly date)
        {
            return await _context.Notes
                .FirstOrDefaultAsync(n => n.UserId == userId &&
                                         n.SubjectGroupId == subjectGroupId &&
                                         n.TargetDate == date);
        }

        // get note by id
        public async Task<Note?> GetByIdAsync(Guid noteId)
        {
            return await _context.Notes.FindAsync(noteId);
        }

        // create 
        public async Task CreateNoteAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
        }

        // update
        public async Task UpdateNoteAsync(Note note, NoteRequestDto dto)
        {
            note.Title = dto.Title;
            note.Content = dto.Content;

            await _context.SaveChangesAsync();
        }

        // delete
        public async Task DeleteAsync(Note note)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
    }
}
