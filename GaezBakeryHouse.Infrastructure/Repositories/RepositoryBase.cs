using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GaezBakeryHouse.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context) =>
            _context = context;

        public IQueryable<T> GetAll() =>
            _context.Set<T>().AsNoTracking();

        public async Task<T> GetByIdAsync(int id) =>
            await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task PostAsync(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
