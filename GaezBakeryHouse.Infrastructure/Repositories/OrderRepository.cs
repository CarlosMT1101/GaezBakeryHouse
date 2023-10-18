using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GaezBakeryHouse.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<OrderDetail> GetOrderDetail(int orderId) =>
            _context.OrderDetails
            .AsNoTracking()
            .Include(x => x.Order)
            .Include(x => x.Product)
            .Where(x => x.OrderId == orderId)
            .AsQueryable();

        public IQueryable<Order> GetOrdersByUser(string userId) =>
            _context.Orders
            .AsNoTracking()
            .Where(x => x.ApplicationUserId == userId)
            .OrderByDescending(x => x.OrderPlaced);
    }
}
