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
    }
}
