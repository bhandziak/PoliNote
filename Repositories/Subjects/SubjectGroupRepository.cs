using Microsoft.EntityFrameworkCore;
using PoliNote.Data;
using PoliNote.DTOs.Subjects.Requests;
using PoliNote.DTOs.Subjects.Responses;
using PoliNote.Models.Subjects;

namespace PoliNote.Repositories.Subjects
{
    public class SubjectGroupRepository(AppDbContext context) : Repository(context)
    {
        public async Task<List<SubjectGroupDto>> GetGroupsWithEnrollmentStatusAsync(Guid userId)
        {
            return await _context.SubjectGroups
                .Select(g => new SubjectGroupDto
                {
                    Id = g.Id,
                    IsEnrolled = g.Enrollments.Any(e => e.UserId == userId), // if user is enrolled

                    Name = g.Subject.Name,
                    Etcs = g.Subject.Etcs,
                    TeacherName = g.Subject.LecturerName,

                    GroupName = g.GroupName,
                    StartTime = g.StartTime.ToString("HH:mm"),
                    EndTime = g.StartTime.Add(g.Duration).ToString("HH:mm")
                })
                .ToListAsync();
        }

        public async Task<SubjectGroupDetailsDto?> GetGroupDetailsAsync(Guid groupId, Guid userId)
        {
            return await _context.SubjectGroups
                .Where(g => g.Id == groupId)
                .Select(g => new SubjectGroupDetailsDto
                {
                    Id = g.Id,
                    IsEnrolled = g.Enrollments.Any(e => e.UserId == userId),

                    // Subject
                    Name = g.Subject.Name,
                    Etcs = g.Subject.Etcs,
                    SyllabusUrl = g.Subject.SyllabusUrl,
                    LecturerName = g.Subject.LecturerName,

                    //  SubjectGroup
                    GroupName = g.GroupName,
                    Location = g.Location,
                    TeacherName = g.TeacherName,
                    Frequency = g.Frequency.ToString(),

                    StartTime = g.StartTime.ToString("HH:mm"),
                    EndTime = g.StartTime.Add(g.Duration).ToString("HH:mm")
                })
                .FirstOrDefaultAsync();
        }
        public async Task<SubjectGroup> CreateGroupAsync(Guid subjectId, SubjectGroupRequestDto dto)
        {
            // check if subject exists
            var subjectExists = await _context.Subjects.AnyAsync(s => s.Id == subjectId);
            if (!subjectExists)
                throw new KeyNotFoundException("Subject not found.");

            var newGroup = new SubjectGroup
            {
                Id = Guid.NewGuid(),
                SubjectId = subjectId,
                GroupName = dto.GroupName,
                Location = dto.Location,
                TeacherName = dto.TeacherName,
                Frequency = dto.Frequency,
                FirstOccurrence = dto.FirstOccurrence,
                LastOccurrence = dto.LastOccurrence,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                Duration = TimeSpan.FromMinutes(dto.DurationMinutes)
            };

            await _context.SubjectGroups.AddAsync(newGroup);
            await _context.SaveChangesAsync();

            return newGroup;
        }
        
    }
}
