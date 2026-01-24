using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliNote.DTOs.Notes;
using PoliNote.Models.Notes;
using PoliNote.Repositories.Notes;
using PoliNote.Repositories.Subjects;
using PoliNote.Services;
using PoliNote.Services.auth;
using PoliNote.Services.Auth;
using PoliNote.Services.Notes;

namespace PoliNote.Controllers.Notes
{
    [Route("api/note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteRepository _noteRepo;
        private readonly AuthService _authService;
        private readonly IsOwnerService _isOwnerService;
        private readonly IDataValidator<DateTime?> _dateValidator;
        private readonly NoteValidator _noteValidator;

        public NoteController(
            NoteRepository noteRepo,
            AuthService authService,
            IsOwnerService isOwnerService,
            IDataValidator<DateTime?> dateValidator,
            NoteValidator noteValidator)
        {
            _noteRepo = noteRepo;
            _authService = authService;
            _isOwnerService = isOwnerService;
            _dateValidator = dateValidator;
            _noteValidator = noteValidator;
        }

        // GET /api/subjects/groups/{subjectGroupId}/note?date=YYYY-MM-DD
        [HttpGet("/api/subjects/groups/{subjectGroupId:guid}/note")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<NoteDto>> GetNote(Guid subjectGroupId, [FromQuery] DateTime? date)
        {
            var userId = _authService.GetCurrentUserId();
            if (userId == null) return Unauthorized("User is not logged in");

            _dateValidator.ValidateOrThrow(date);

            await _noteValidator.ValidateAccessOrThrowAsync(userId.Value, subjectGroupId, date);

            var targetDate = DateOnly.FromDateTime(date!.Value);
            var note = await _noteRepo.GetNoteAsync(userId.Value, subjectGroupId, targetDate);

            if (note == null)
                return NotFound("No note found for this lesson.");

            return Ok(new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                CreatedAt = note.CreatedAt
            });
        }

        // POST /api/subjects/groups/{subjectGroupId}/note?date=YYYY-MM-DD
        [HttpPost("/api/subjects/groups/{subjectGroupId:guid}/note")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<NoteDto>> CreateNote(
            Guid subjectGroupId,
            [FromQuery] DateTime? date,
            [FromBody] NoteRequestDto dto)
        {
            var userId = _authService.GetCurrentUserId();
            if (userId == null) return Unauthorized("User is not logged in");

            _dateValidator.ValidateOrThrow(date);

            await _noteValidator.ValidateAccessOrThrowAsync(userId.Value, subjectGroupId, date);


            var targetDate = DateOnly.FromDateTime(date!.Value);

            // Check if note already exists
            var existingNote = await _noteRepo.GetNoteAsync(userId.Value, subjectGroupId, targetDate);
            if (existingNote != null)
            {
                return Conflict("A note for this lesson already exists. Use PATCH to edit it.");
            }

            var note = new Note
            {
                UserId = userId.Value,
                SubjectGroupId = subjectGroupId,
                TargetDate = targetDate,
                Title = dto.Title,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _noteRepo.CreateNoteAsync(note);

            return Ok(new { id = note.Id });
        }

        // PATCH: /api/note/{noteId}
        [HttpPatch("{noteId:guid}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> UpdateNote(Guid noteId, [FromBody] NoteRequestDto dto)
        {
            var note = await _noteRepo.GetByIdAsync(noteId);
            if (note == null) return NotFound("Note not found.");

            if (!_isOwnerService.IsOwner(note.UserId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Only the owner can modify this note.");
            }

            note.Title = dto.Title;
            note.Content = dto.Content;

            await _noteRepo.UpdateAsync(note);
            return NoContent();
        }

        // DELETE /api/note/{noteId}
        [HttpDelete("{noteId:guid}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> DeleteNote(Guid noteId)
        {
            var note = await _noteRepo.GetByIdAsync(noteId);
            if (note == null) return NotFound("Note not found.");

            if (!_isOwnerService.IsOwner(note.UserId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Only the owner can delete this note.");
            }

            await _noteRepo.DeleteAsync(note);

            return Ok(new { message = "Successfully deleted note" });
        }
    }
}
