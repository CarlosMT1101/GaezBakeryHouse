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

        public async Task DeleteAllShoppingCartItemsByUserId(string userId)
        {
            var items = _context.ShoppingCarItems.Where(x => x.ApplicationUserId == userId);

            _context.RemoveRange(items);

            await _context.SaveChangesAsync();
        }

        public async Task<ShoppingCartItem> GetShoppingCartItemAsync(int id, int productId, string userId) =>
            await _context.ShoppingCarItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id &&
                                     x.ProductId == productId &&
                                     x.ApplicationUserId == userId);

        public IQueryable<ShoppingCartItem> GetShoppingCartItemsByUserId(string id) =>
            _context.ShoppingCarItems
            .Include(x => x.Product)
            .AsNoTracking()
            .Where(x => x.ApplicationUserId == id)
            .AsQueryable();
    }
}
