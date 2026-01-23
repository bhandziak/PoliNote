using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.Models.Subjects;

namespace PoliNote.Repositories.Subjects
{
    public class EnrollmentRepository(AppDbContext context) : Repository(context)
    {
        // enroll student to group in subject
        public async Task EnrollStudentAsync(Guid userId, Guid groupId)
        {
            // group exits check
            var groupExists = await _context.SubjectGroups.AnyAsync(g => g.Id == groupId);
            if (!groupExists) throw new KeyNotFoundException("Group not found.");

            // allready enrolled check
            var alreadyEnrolled = await _context.Enrollments
                .AnyAsync(e => e.UserId == userId && e.SubjectGroupId == groupId);

            if (alreadyEnrolled)
                throw new InvalidOperationException("You are already enrolled in this group.");

            var enrollment = new Enrollment
            {
                UserId = userId,
                SubjectGroupId = groupId,
                Absences = 0
            };

            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

        // unenroll student from group in subject
        public async Task UnenrollStudentAsync(Guid userId, Guid groupId)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.UserId == userId && e.SubjectGroupId == groupId);

            // not enrolled check
            if (enrollment == null)
            {
                throw new KeyNotFoundException("You are not enrolled in this group.");
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
        }

        // get subjectGroups by date, by user enrollmets
        // needs later filtering (frequency)
        public async Task<List<SubjectGroup>> GetUserEnrolledGroupsForDayAsync(Guid userId, DateOnly date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;

            return await _context.Enrollments
                .Where(e => e.UserId == userId) // by userId
                .Include(e => e.SubjectGroup) // join SubjectGroup
                    .ThenInclude(g => g.Subject) // join Subject
                .Select(e => e.SubjectGroup) 
                .Where(g => g.DayOfWeek == dayOfWeek && // by date
                            date >= g.FirstOccurrence &&
                            date <= g.LastOccurrence)
                .ToListAsync();
        }

        // update student absences to groupSubject
        public async Task UpdateAbsencesAsync(Guid userId, Guid groupId, int count)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.UserId == userId && e.SubjectGroupId == groupId);

            if (enrollment == null)
            {
                throw new KeyNotFoundException("You are not enrolled in this group.");
            }

            enrollment.Absences = count;
            await _context.SaveChangesAsync();
        }
    }
}
