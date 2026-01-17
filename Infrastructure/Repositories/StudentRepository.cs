using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.Class)
                .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> AddAsync(Student entity)
        {
            _context.Students.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Student> UpdateAsync(Student entity)
        {
            _context.Students.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Students.FindAsync(id);
            if (entity == null)
                return false;

            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
