using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Application.Contracts
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        IQueryable<Order> GetOrdersByUser(string userId);

        IQueryable<OrderDetail> GetOrderDetail(int orderId);
    }
}
