using PoliNote.DTOs.PrivateCalendar;
using PoliNote.Models.Calendar;
using PoliNote.Models.Subjects;
using PoliNote.Repositories.Calendar;
using PoliNote.Repositories.Subjects;

namespace PoliNote.Services.Calendar
{
    public class PrivateCalendarService
    {
        private readonly PrivateCalendarRepository _privateCalendarRepo;
        private readonly EnrollmentRepository _enrollmentRepo;

        public PrivateCalendarService(
            PrivateCalendarRepository privateCalendarRepo,
            EnrollmentRepository enrollmentRepo)
        {
            _privateCalendarRepo = privateCalendarRepo;
            _enrollmentRepo = enrollmentRepo;
        }

        public async Task<List<PrivateEventDto>> GetFullCalendarForDateAsync(DateTime date, Guid userId)
        {
            var targetDate = DateOnly.FromDateTime(date);

            // Fetch data
            var privateEventsList = await _privateCalendarRepo.GetByDateAsync(date, userId);
            var enrolledGroupsList = await _enrollmentRepo.GetUserEnrolledGroupsForDayAsync(userId, targetDate);


            // process subjects events
            var subjectEvents = enrolledGroupsList
                .Where(g => IsOccurringOn(g, targetDate))
                .Select(g => new PrivateEventDto
                {
                    Id = g.Id,
                    Title = $"{g.Subject.Name} ({g.GroupName})",
                    Date = date,
                    TimeString = $"{g.StartTime:HH:mm} - {g.StartTime.Add(g.Duration):HH:mm}",
                    Location = g.Location,
                    EventType = PrivateEventType.Subject,
                    IsSubject = true
                });

            // process events
            var privateEvents = privateEventsList.Select(e => new PrivateEventDto
            {
                Id = e.Id,
                Title = e.Title,
                Date = e.Date,
                TimeString = $"{e.Time.Hours:D2}:{e.Time.Minutes:D2}",
                Location = e.Location,
                EventType = e.EventType,
                IsSubject = false
            });

            return subjectEvents.Concat(privateEvents)
                .OrderBy(e => e.TimeString)
                .ToList();
        }

        // filters SubjectGroup by its frequency (1, 2, 4 a week)
        private bool IsOccurringOn(SubjectGroup group, DateOnly date)
        {
            int daysDiff = date.DayNumber - group.FirstOccurrence.DayNumber;
            int weeksDiff = daysDiff / 7;

            return weeksDiff % (int)group.Frequency == 0;
        }
    }
}
