using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        // GET: api/classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetAllClasses()
        {
            var classes = await _classService.GetAllClassesAsync();
            return Ok(classes);
        }

        // GET: api/classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClassById(int id)
        {
            var classEntity = await _classService.GetClassByIdAsync(id);
            if (classEntity == null)
            {
                return NotFound(new { message = $"Không tìm thấy lớp với Id = {id}" });
            }
            return Ok(classEntity);
        }

        // POST: api/classes
        [HttpPost]
        public async Task<ActionResult<Class>> CreateClass([FromBody] Class classEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdClass = await _classService.CreateClassAsync(classEntity);
            return CreatedAtAction(nameof(GetClassById), new { id = createdClass.Id }, createdClass);
        }
    }
}
