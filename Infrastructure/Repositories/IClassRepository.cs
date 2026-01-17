using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllAsync();
        Task<Class?> GetByIdAsync(int id);
        Task<Class> AddAsync(Class entity);
        Task<Class> UpdateAsync(Class entity);
        Task<bool> DeleteAsync(int id);
    }
}
