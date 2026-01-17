using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliNote.DTOs.PublicCalendar;
using PoliNote.Models;
using PoliNote.Repositories;
using PoliNote.Services.auth;
using PoliNote.Services.Auth;
using PoliNote.Services.PublicCalendar;

namespace PoliNote.Controllers
{
    [Route("api/calendar/public")]
    [ApiController]
    public class PublicCalendarController : ControllerBase
    {
        private readonly PublicCalendarRepository _publicCalendarRepo;
        private readonly AuthService _authService;
        private readonly PublicEventValidator _validator;
        private readonly IsOwnerService _isOwnerService;

        public PublicCalendarController(
            PublicCalendarRepository publicCalendarRepo,
            AuthService authService,
            PublicEventValidator publicEventValidator,
            IsOwnerService isOwnerService
            )
        {
            _publicCalendarRepo = publicCalendarRepo;
            _authService = authService;
            _validator = publicEventValidator;
            _isOwnerService = isOwnerService;
        }


        // GET api/calendar/public?date=YYYY-MM-DD
        [HttpGet]
        public async Task<IActionResult> GetByDate([FromQuery] DateTime? date)
        {
            if (date == null)
            {
                return BadRequest(new { message = "Date is required. Use YYYY-MM-DD format." });
            }

            if (date.Value == default)
            {
                return BadRequest(new { message = "Invalid date format provided." });
            }

            var events = await _publicCalendarRepo.GetByDataAsync(date.Value);

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
            _validator.ValidateEvent(request);

            var userId = _authService.GetCurrentUserId();

            if (userId == null)
                return Unauthorized("User is not logged in"); 

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
            return CreatedAtAction(nameof(GetById), new { id = newEvent.Id }, newEvent);
        }

        // PATCH api/calendar/public/{id}
        [HttpPatch("{id:guid}")]
        [Authorize(Roles = "Admin,Informant")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PublicEventRequestDto request)
        {
            _validator.ValidateEvent(request);

            var existingEvent = await _publicCalendarRepo.GetByIdAsync(id);
            if (existingEvent == null) return NotFound();

            if (!_isOwnerService.CanUserEditOrDelete(existingEvent.CreatedByUserId))
                return Forbid();

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

            if (!_isOwnerService.CanUserEditOrDelete(existingEvent.CreatedByUserId))
                return Forbid();

            // delete
            await _publicCalendarRepo.DeleteAsync(id);

            return Ok();
        }
    }
}
