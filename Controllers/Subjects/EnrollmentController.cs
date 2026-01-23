using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliNote.DTOs.Subjects.Requests;
using PoliNote.Repositories.Subjects;
using PoliNote.Services;
using PoliNote.Services.auth;

namespace PoliNote.Controllers.Subjects
{
    [Route("api/subjects/groups")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollmentRepository _enrollRepo;
        private readonly AuthService _authService;

        public EnrollmentController(
            EnrollmentRepository enrollRepo,
            AuthService authService)
        {
            _enrollRepo = enrollRepo;
            _authService = authService;
        }

        // POST api/subjects/groups/{subjectGroupId}/enroll
        [HttpPost("{subjectGroupId:guid}/enroll")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Enroll(Guid subjectGroupId)
        {
            var userId = _authService.GetCurrentUserId();
            if (userId == null) return Unauthorized();

            try
            {
                await _enrollRepo.EnrollStudentAsync(userId.Value, subjectGroupId);
                return Ok(new { message = "Successfully enrolled in the group." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/subjects/groups/{subjectGroupId}/unenroll
        [HttpDelete("{subjectGroupId:guid}/unenroll")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Unenroll(Guid subjectGroupId)
        {
            var userId = _authService.GetCurrentUserId();
            if (userId == null) return Unauthorized();

            try
            {
                await _enrollRepo.UnenrollStudentAsync(userId.Value, subjectGroupId);
                return Ok(new { message = "Successfully unenrolled from the group." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
