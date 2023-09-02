using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Application.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsByCategory(int categoryId);

        Task<Product> GetProductById(int productId);
    }
}
