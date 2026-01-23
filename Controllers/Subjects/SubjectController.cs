using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliNote.DTOs.Subjects.Requests;
using PoliNote.Models.Subjects;
using PoliNote.Repositories.Subjects;
using PoliNote.Services;
using PoliNote.Services.auth;

namespace PoliNote.Controllers.Subjects
{
    [Route("api/subjects")]
    [ApiController]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly SubjectRepository _subjectRepo;
        private readonly AuthService _authService;
        private readonly IDataValidator<SubjectRequestDto> _validator;

        public SubjectController(
            SubjectRepository subjectRepo,
            AuthService authService,
            IDataValidator<SubjectRequestDto> validator)
        {
            _subjectRepo = subjectRepo;
            _authService = authService;
            _validator = validator;
        }

        // GET api/subjects
        [HttpGet]
        [Authorize(Roles = "Student,Admin,Informant")]
        public async Task<IActionResult> GetAll() 
        { 
            var subjects = await _subjectRepo.GetAllAsync();
            return Ok(subjects);
        }

        // GET: api/subjects/{subjectId}
        [HttpGet("{subjectId:guid}")]
        [Authorize(Roles = "Student,Admin,Informant")]
        public async Task<IActionResult> GetById(Guid subjectId)
        {
            var userId = _authService.GetCurrentUserId();
                if (userId == null) return Unauthorized();

            var subject = await _subjectRepo.GetByIdWithGroupsAsync(subjectId, userId.Value);
            if (subject == null) return NotFound();
            return Ok(subject);
        }

        // POST: api/subjects
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] SubjectRequestDto request)
        {
            _validator.ValidateOrThrow(request);

            var subject = new Subject
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Etcs = request.Etcs,
                SyllabusUrl = request.SyllabusUrl,
                LecturerName = request.LecturerName
            };

            await _subjectRepo.AddAsync(subject);

            return Ok( new { id = subject.Id });
        }

        // PATCH: api/subjects/{subjectId}
        [HttpPatch("{subjectId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid subjectId, [FromBody] SubjectRequestDto request)
        {
            var subject = await _subjectRepo.GetByIdAsync(subjectId);
            if (subject == null)
            {
                return NotFound(new { message = $"Subject with ID {subjectId} not found." });
            }

            _validator.ValidateOrThrow(request);

            subject.Name = request.Name;
            subject.Etcs = request.Etcs;
            subject.SyllabusUrl = request.SyllabusUrl;
            subject.LecturerName = request.LecturerName;

            await _subjectRepo.UpdateAsync(subject);

            return NoContent();
        }

        // DELETE: api/subjects/{subjectId}
        [HttpDelete("{subjectId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid subjectId)
        {
            try
            {
                await _subjectRepo.DeleteAsync(subjectId);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
