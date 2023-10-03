using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GaezBakeryHouse.Infrastructure.Repositories
{
    public class ShoppingCartItemRepository : RepositoryBase<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<ShoppingCartItem> GetShoppingCartItemsByUserId(string id) =>
            _context.ShoppingCarItems
            .AsNoTracking()
            .Where(x => x.ApplicationUserId == id)
            .AsQueryable();
    }
}
