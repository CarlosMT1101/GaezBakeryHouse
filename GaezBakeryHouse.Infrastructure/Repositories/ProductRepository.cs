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

        public IQueryable<Product> GetProductsByCategory(int categoryId) =>
            _context.Products
            .AsNoTracking()
            .Where(x => x.CategoryId == categoryId)
            .AsQueryable();

        public IQueryable<Product> GetTrendingProducts() =>
            _context.Products
            .AsNoTracking()
            .Where(x => x.IsTrendingProduct == true)
            .Take(6)
            .OrderBy(x => x.Name)
            .AsQueryable();
    }
}
