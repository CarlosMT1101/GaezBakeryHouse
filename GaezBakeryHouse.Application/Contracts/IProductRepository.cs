using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Application.Contracts
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategory(int categoryId);

        Task<IEnumerable<Product>> GetProductsInOffer();

        Task<IEnumerable<Product>> GetProductsInOfferByCategory(int categoryId);
    }
}
