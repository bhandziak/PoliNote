using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliNote.DTOs.PrivateCalendar;
using PoliNote.DTOs.PublicCalendar;
using PoliNote.Models.Calendar;
using PoliNote.Models.Subjects;
using PoliNote.Repositories.Calendar;
using PoliNote.Services;
using PoliNote.Services.auth;
using PoliNote.Services.Auth;
using PoliNote.Services.Calendar;

namespace PoliNote.Controllers.Calendar
{
    [Route("api/calendar/private")]
    [ApiController]
    [Authorize]
    public class PrivateCalendarController : ControllerBase
    {
        private readonly PrivateCalendarRepository _privateCalendarRepo;

        private readonly AuthService _authService;
        private readonly IsOwnerService _isOwnerService;
        private readonly PrivateCalendarService _privateCalendarService;

        private readonly IDataValidator<PrivateEventRequestDto> _eventValidator;
        private readonly IDataValidator<DateTime?> _dateValidator;

        public PrivateCalendarController(
            PrivateCalendarRepository privateCalendarRepo,
            AuthService authService,
            IsOwnerService isOwnerService,
            PrivateCalendarService privateCalendarService,
            IDataValidator<PrivateEventRequestDto> eventValidator,
            IDataValidator<DateTime?> dateValidator
            )
        {
            _privateCalendarRepo = privateCalendarRepo;
            _authService = authService;
            _isOwnerService = isOwnerService;
            _privateCalendarService = privateCalendarService;
            _eventValidator = eventValidator;
            _dateValidator = dateValidator;
        }

        // GET api/calendar/private?date=YYYY-MM-DD
        [HttpGet]
        [Authorize(Roles = "Student,Admin,Informant")]
        public async Task<IActionResult> GetByDate([FromQuery] DateTime? date)
        {
            Guid? userId = _authService.GetCurrentUserId();
            if( userId == null )
                return Unauthorized("User is not logged in");

            _dateValidator.ValidateOrThrow(date);

            var result = await _privateCalendarService.GetFullCalendarForDateAsync(date.Value, userId.Value);

            return Ok(result);
        }

        // GET api/calendar/private/{privateEventId}
        [HttpGet("{privateEventId:guid}")]
        [Authorize(Roles = "Student,Admin,Informant")]
        public async Task<IActionResult> GetById(Guid privateEventId)
        {
            var privateEvent = await _privateCalendarRepo.GetByIdAsync(privateEventId);

            if (privateEvent == null) return NotFound("Private event not found");

            if (!_isOwnerService.IsOwner(privateEvent.CreatedByUserId))
                return Forbid();

            var dto = new PrivateEventDetailsDto
            {
                Id = privateEvent.Id,
                Title = privateEvent.Title,
                Date = privateEvent.Date,
                Time = privateEvent.Time,
                Description = privateEvent.Description,
                Location = privateEvent.Location,
                EventType = privateEvent.EventType
            };

            return Ok(dto);
        }

        // POST api/calendar/private
        [HttpPost]
        [Authorize(Roles = "Student,Admin,Informant")]
        public async Task<IActionResult> Create([FromBody] PrivateEventRequestDto request)
        {
            var userId = _authService.GetCurrentUserId();

            if (userId == null)
                return Unauthorized("User is not logged in");

            _eventValidator.ValidateOrThrow(request);

            if (!Enum.TryParse<PrivateEventType>(request.EventType, true, out var eventTypeEnum))
            {
                return BadRequest($"Invalid event type: {request.EventType}");
            }

            var newEvent = new PrivateEvent
            {
                Id = Guid.NewGuid(),
                CreatedByUserId = userId.Value,
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                Time = request.Time,
                Location = request.Location,
                EventType = eventTypeEnum
            };

            await _privateCalendarRepo.PutAsync(newEvent);

            return Ok(new { id = newEvent.Id });
        }

        // PATCH api/calendar/private/{privateEventId}
        [HttpPatch("{privateEventId:guid}")]
        [Authorize(Roles = "Student,Admin,Informant")]
        public async Task<IActionResult> Update(Guid privateEventId, [FromBody] PrivateEventRequestDto request)
        {
            var existingEvent = await _privateCalendarRepo.GetByIdAsync(privateEventId);
            if (existingEvent == null) return NotFound("Private event not found");

            if (!_isOwnerService.IsOwner(existingEvent.CreatedByUserId))
                return Forbid("Only owner can edit event.");

            _eventValidator.ValidateOrThrow(request);
            if (!Enum.TryParse<PrivateEventType>(request.EventType, true, out var eventTypeEnum))
            {
                return BadRequest($"Invalid event type: {request.EventType}");
            }

            // update fields
            existingEvent.Title = request.Title;
            existingEvent.Description = request.Description;
            existingEvent.Date = request.Date;
            existingEvent.Time = request.Time;
            existingEvent.Location = request.Location;
            existingEvent.EventType = eventTypeEnum;

            // save
            await _privateCalendarRepo.UpdateAsync(existingEvent);

            return NoContent();
        }

        // DELETE api/calendar/private/{privateEventId}
        [HttpDelete("{privateEventId:guid}")]
        [Authorize(Roles = "Student,Admin,Informant")]
        public async Task<IActionResult> Delete(Guid privateEventId)
        {
            var existingEvent = await _privateCalendarRepo.GetByIdAsync(privateEventId);
            if (existingEvent == null) return NotFound("Private event not found");

            if (!_isOwnerService.IsOwner(existingEvent.CreatedByUserId))
                return Forbid("Only owner can delete event.");

            // delete
            await _privateCalendarRepo.DeleteAsync(privateEventId);

            return Ok(new { message = "Successfully deleted private event" });
        }
    }
}
