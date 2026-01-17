using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound(new { message = $"Không tìm thấy sinh viên với Id = {id}" });
            }
            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdStudent = await _studentService.CreateStudentAsync(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, createdStudent);
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, [FromBody] Student student)
        {
            if (id != student.Id)
            {
                return BadRequest(new { message = "Id không khớp" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStudent = await _studentService.GetStudentByIdAsync(id);
            if (existingStudent == null)
            {
                return NotFound(new { message = $"Không tìm thấy sinh viên với Id = {id}" });
            }

            var updatedStudent = await _studentService.UpdateStudentAsync(student);
            return Ok(updatedStudent);
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (!result)
            {
                return NotFound(new { message = $"Không tìm thấy sinh viên với Id = {id}" });
            }
            return Ok(new { message = "Xóa sinh viên thành công" });
        }
    }
}
