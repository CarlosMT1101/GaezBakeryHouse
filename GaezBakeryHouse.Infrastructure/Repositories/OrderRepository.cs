using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
