using IquiriumBE.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IquiriumBE.Infrastructure.Abstractions
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;

        protected Repository(DbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
    }
}
