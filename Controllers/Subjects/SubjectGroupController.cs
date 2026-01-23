using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliNote.DTOs.Subjects.Requests;
using PoliNote.DTOs.Subjects.Responses;
using PoliNote.Repositories.Subjects;
using PoliNote.Services;
using PoliNote.Services.auth;
using System.ComponentModel.DataAnnotations;

namespace PoliNote.Controllers.Subjects
{
    [ApiController]
    [Route("api/subjects/groups")]
    [Authorize]
    public class SubjectGroupController : ControllerBase
    {
        private readonly SubjectGroupRepository _groupRepo;
        private readonly AuthService _authService;
        private readonly IDataValidator<SubjectGroupRequestDto> _validator;

        public SubjectGroupController(
            SubjectGroupRepository groupRepo,
            AuthService authService,
            IDataValidator<SubjectGroupRequestDto> validator)
        {
            _groupRepo = groupRepo;
            _authService = authService;
            _validator = validator;
        }

        // GET api/subjects/groups
        [HttpGet]
        [Authorize(Roles = "Student,Admin,Informant")]
        public async Task<ActionResult<List<SubjectGroupDto>>> GetAll()
        {
            var userId = _authService.GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var groups = await _groupRepo.GetGroupsWithEnrollmentStatusAsync(userId.Value);

            return Ok(groups);
        }

        // GET api/subjects/groups/{subjectGroupId}
        [HttpGet("{subjectGroupId:guid}")]
        [Authorize(Roles = "Student,Admin,Informant")]
        public async Task<ActionResult<SubjectGroupDetailsDto>> GetById(Guid subjectGroupId)
        {
            var userId = _authService.GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var groupDetails = await _groupRepo.GetGroupDetailsAsync(subjectGroupId, userId.Value);

            if (groupDetails == null)
            {
                return NotFound(new { message = "Subject group not found." });
            }

            return Ok(groupDetails);
        }

        // POST api/subjects/{subjectId}/groups
        [HttpPost("/api/subjects/{subjectId:guid}/groups")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Guid subjectId, [FromBody] SubjectGroupRequestDto request)
        {
            _validator.ValidateOrThrow(request);

            try
            {
                var createdGroup = await _groupRepo.CreateGroupAsync(subjectId, request);

                return Ok(new { id = createdGroup.Id });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // PATCH api/subjects/groups/{subjectGroupId}
        [HttpPatch("{subjectGroupId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid subjectGroupId, [FromBody] SubjectGroupRequestDto request)
        {
            _validator.ValidateOrThrow(request);

            try
            {
                var updatedGroup = await _groupRepo.UpdateGroupAsync(subjectGroupId, request);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE api/subjects/groups/{subjectGroupId}
        [HttpDelete("{subjectGroupId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid subjectGroupId)
        {
            try
            {
                await _groupRepo.DeleteAsync(subjectGroupId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
