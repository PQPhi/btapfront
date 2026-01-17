using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<Student> CreateStudentAsync(Student entity)
        {
            return await _studentRepository.AddAsync(entity);
        }

        public async Task<Student> UpdateStudentAsync(Student entity)
        {
            return await _studentRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteAsync(id);
        }
    }
}
