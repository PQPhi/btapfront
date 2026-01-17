using Domain.Entities;

namespace Application.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student> CreateStudentAsync(Student entity);
        Task<Student> UpdateStudentAsync(Student entity);
        Task<bool> DeleteStudentAsync(int id);
    }
}
