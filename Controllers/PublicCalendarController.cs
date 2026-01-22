using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliNote.DTOs.PublicCalendar;
using PoliNote.Models;
using PoliNote.Repositories;
using PoliNote.Services;
using PoliNote.Services.auth;
using PoliNote.Services.Auth;
using PoliNote.Services.Calendar;
using PoliNote.Services.PublicCalendar;

namespace PoliNote.Controllers
{
    [Route("api/calendar/public")]
    [ApiController]
    public class PublicCalendarController : ControllerBase
    {
        private readonly PublicCalendarRepository _publicCalendarRepo;
        private readonly AuthService _authService;
        private readonly IDataValidator<PublicEventRequestDto> _eventValidator;
        private readonly IDataValidator<DateTime?> _dateValidator;
        private readonly IsOwnerService _isOwnerService;

        public PublicCalendarController(
            PublicCalendarRepository publicCalendarRepo,
            AuthService authService,
            IDataValidator<DateTime?> dateValidator,
            IDataValidator<PublicEventRequestDto> validator,
            IsOwnerService isOwnerService
            )
        {
            _publicCalendarRepo = publicCalendarRepo;
            _authService = authService;
            _eventValidator = validator;
            _dateValidator = dateValidator;
            _isOwnerService = isOwnerService;
        }


        // GET api/calendar/public?date=YYYY-MM-DD
        [HttpGet]
        public async Task<IActionResult> GetByDate([FromQuery] DateTime? date)
        {
            _dateValidator.ValidateOrThrow(date);

            var events = await _publicCalendarRepo.GetByDateAsync(date.Value);

            var eventDtos = events.Select(e => new PublicEventDto
            {
                Id = e.Id,
                Title = e.Title,
                Date = e.Date,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Location = e.Location
            }).ToList();

            return Ok(eventDtos);
        }

        // GET api/calendar/public/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var publicEvent = await _publicCalendarRepo.GetByIdAsync(id);

            if(publicEvent == null) return NotFound();

            string creatorUsername = publicEvent.CreatedByUser?.Username ?? "Unknown";
            string creatorNameAndSurname = $"{publicEvent.CreatedByUser?.FirstName} {publicEvent.CreatedByUser?.LastName}".Trim()
                ?? "Unknown";

            var dto = new PublicEventDetailsDto
            {
                Id = publicEvent.Id,

                CreatedByUsername = creatorUsername,
                CreatedByNameAndSurname = creatorNameAndSurname,
                
                Title = publicEvent.Title,
                Date = publicEvent.Date,
                Description = publicEvent.Description,
                StartTime = publicEvent.StartTime,
                EndTime = publicEvent.EndTime,
                Location = publicEvent.Location
            };

            return Ok(dto);
        }

        // POST api/calendar/public
        [HttpPost]
        [Authorize(Roles = "Admin,Informant")]
        public async Task<IActionResult> Create([FromBody] PublicEventRequestDto request)
        {
            var userId = _authService.GetCurrentUserId();

            if (userId == null)
                return Unauthorized("User is not logged in");

            _eventValidator.ValidateOrThrow(request);

            var newEvent = new PublicEvent
            {
                Id = Guid.NewGuid(),
                CreatedByUserId = userId.Value,
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Location = request.Location
            };

            await _publicCalendarRepo.PutAsync(newEvent);

            return Ok(new { id = newEvent.Id });
        }

        // PATCH api/calendar/public/{id}
        [HttpPatch("{id:guid}")]
        [Authorize(Roles = "Admin,Informant")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PublicEventRequestDto request)
        {
            var existingEvent = await _publicCalendarRepo.GetByIdAsync(id);
            if (existingEvent == null) return NotFound();

            if (!_isOwnerService.IsOwnerOrAdmin(existingEvent.CreatedByUserId))
                return Forbid();

            _eventValidator.ValidateOrThrow(request);

            // update fields
            existingEvent.Title = request.Title;
            existingEvent.Description = request.Description;
            existingEvent.Date = request.Date;
            existingEvent.StartTime = request.StartTime;
            existingEvent.EndTime = request.EndTime;
            existingEvent.Location = request.Location;

            // save
            await _publicCalendarRepo.UpdateAsync(existingEvent);

            return NoContent();
        }

        // DELETE api/calendar/public/{id}
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin,Informant")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingEvent = await _publicCalendarRepo.GetByIdAsync(id);
            if (existingEvent == null) return NotFound();

            if (!_isOwnerService.IsOwnerOrAdmin(existingEvent.CreatedByUserId))
                return Forbid();

            // delete
            await _publicCalendarRepo.DeleteAsync(id);

            return Ok();
        }
    }
}
