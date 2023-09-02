using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GaezBakeryHouse.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
            => _context = context;

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId);

            if (product == null)
            {
                return new Product();
            }

            return product;
        }
        public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
        {
            var products = await _context.Products
                .AsNoTracking()
                .Where(product => product.CategoryId == categoryId)
                .ToListAsync();

            return products;
        }
    }
}
