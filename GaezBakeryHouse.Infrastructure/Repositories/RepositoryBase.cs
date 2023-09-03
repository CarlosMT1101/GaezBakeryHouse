using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Common;
using GaezBakeryHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GaezBakeryHouse.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context) =>
            _context = context;

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
