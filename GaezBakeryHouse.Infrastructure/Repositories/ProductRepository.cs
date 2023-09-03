using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GaezBakeryHouse.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(product => product.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
