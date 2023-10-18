using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Application.Contracts
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        IQueryable<Product> GetProductsByCategory(int categoryId);
        IQueryable<Product> GetTrendingProducts();
    }
}
