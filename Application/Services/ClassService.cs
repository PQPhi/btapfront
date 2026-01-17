using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            return await _classRepository.GetAllAsync();
        }

        public async Task<Class?> GetClassByIdAsync(int id)
        {
            return await _classRepository.GetByIdAsync(id);
        }

        public async Task<Class> CreateClassAsync(Class entity)
        {
            return await _classRepository.AddAsync(entity);
        }

        public async Task<Class> UpdateClassAsync(Class entity)
        {
            return await _classRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteClassAsync(int id)
        {
            return await _classRepository.DeleteAsync(id);
        }
    }
}
