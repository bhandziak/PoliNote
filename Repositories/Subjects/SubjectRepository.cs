using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.DTOs.Subjects.Responses;
using PoliNote.Models.Subjects;

namespace PoliNote.Repositories.Subjects
{
    public class SubjectRepository(AppDbContext context) : Repository(context)
    {
        // subject details
        public async Task<IEnumerable<SubjectDto>> GetAllAsync()
        {
            return await _context.Subjects
                .OrderBy(s => s.Name)
                .Select(s => new SubjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Etcs = s.Etcs,
                    SyllabusUrl = s.SyllabusUrl,
                    LecturerName = s.LecturerName
                })
                .ToListAsync();
        }

        // subject details by id
        public async Task<Subject?> GetByIdAsync(Guid id) 
        {
            return await _context.Subjects
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
        }

        // fetches subject with their groups
        public async Task<SubjectWithGroupsDto?> GetByIdWithGroupsAsync(Guid subjectId, Guid userId)
        {
            return await _context.Subjects
                .Where(s => s.Id == subjectId)
                .Select(s => new SubjectWithGroupsDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Etcs = s.Etcs,
                    SyllabusUrl = s.SyllabusUrl,
                    LecturerName = s.LecturerName,
                    Groups = s.Groups.Select(g => new SubjectGroupDto
                    {
                        Id = g.Id,
                        GroupName = g.GroupName,
                        TeacherName = g.TeacherName,
                        // subject copy
                        Name = s.Name,
                        Etcs = s.Etcs,
                        // time
                        StartTime = g.StartTime.ToString("HH:mm"),
                        EndTime = g.StartTime.Add(g.Duration).ToString("HH:mm"),
                        // enrollment
                        IsEnrolled = g.Enrollments.Any(e => e.UserId == userId)
                    }).ToList()
                })
                .FirstOrDefaultAsync();
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
