using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            return await _context.Classes
                .Include(c => c.Students)
                .ToListAsync();
        }

        public async Task<Class?> GetByIdAsync(int id)
        {
            return await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Class> AddAsync(Class entity)
        {
            _context.Classes.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Class> UpdateAsync(Class entity)
        {
            _context.Classes.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Classes.FindAsync(id);
            if (entity == null)
                return false;

            _context.Classes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
