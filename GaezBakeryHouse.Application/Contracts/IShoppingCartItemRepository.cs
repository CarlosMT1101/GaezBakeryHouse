using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Application.Contracts
{
    public interface IShoppingCartItemRepository : IAsyncRepository<ShoppingCartItem>
    {
        IQueryable<ShoppingCartItem> GetShoppingCartItemsByUserId(string id);
    }
}
