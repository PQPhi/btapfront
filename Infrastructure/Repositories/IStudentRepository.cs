using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student> AddAsync(Student entity);
        Task<Student> UpdateAsync(Student entity);
        Task<bool> DeleteAsync(int id);
    }
}
