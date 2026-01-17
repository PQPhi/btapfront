using Domain.Entities;

namespace Application.Services
{
    public interface IClassService
    {
        Task<IEnumerable<Class>> GetAllClassesAsync();
        Task<Class?> GetClassByIdAsync(int id);
        Task<Class> CreateClassAsync(Class entity);
        Task<Class> UpdateClassAsync(Class entity);
        Task<bool> DeleteClassAsync(int id);
    }
}
