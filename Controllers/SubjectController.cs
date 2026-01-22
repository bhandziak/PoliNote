using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliNote.Models.Subjects;
using PoliNote.Repositories.Subjects;
using PoliNote.Services;

namespace PoliNote.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly SubjectRepository _subjectRepo;
        private readonly IDataValidator<Subject> _validator;

        public SubjectController(
            SubjectRepository subjectRepo,
            IDataValidator<Subject> validator)
        {
            _subjectRepo = subjectRepo;
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

        // GET: api/subjects/{id}
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Student,Admin,Informant")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var subject = await _subjectRepo.GetByIdAsync(id);
            if (subject == null) return NotFound();
            return Ok(subject);
        }


    }
}
