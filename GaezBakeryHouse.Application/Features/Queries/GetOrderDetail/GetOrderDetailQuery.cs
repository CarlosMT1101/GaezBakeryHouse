using GaezBakeryHouse.Application.DTOs;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetOrderDetail
{
    public class GetOrderDetailQuery : IRequest<IQueryable<OrderDetailDTO>>
    {
        public int OrderId { get; private set; }

        public GetOrderDetailQuery(int id) =>
            OrderId = id;
    }
}
