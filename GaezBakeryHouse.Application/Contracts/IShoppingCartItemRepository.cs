using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Application.Contracts
{
    public interface IShoppingCartItemRepository : IAsyncRepository<ShoppingCartItem>
    {
        IQueryable<ShoppingCartItem> GetShoppingCartItemsByUserId(string id);
        Task<ShoppingCartItem> GetShoppingCartItemAsync(int id, int productId, string userId);
        Task DeleteAllShoppingCartItemsByUserId(string userId);
        Task<decimal> GetUserTotalAmount(string userId);
    }
}
