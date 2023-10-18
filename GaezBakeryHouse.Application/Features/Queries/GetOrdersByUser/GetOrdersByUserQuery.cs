using GaezBakeryHouse.Application.DTOs;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetOrdersByUser
{
    public class GetOrdersByUserQuery : IRequest<IQueryable<OrderDTO>>
    {
        public string Id { get; private set; }

        public GetOrdersByUserQuery(string userId) =>
            Id = userId;
    }
}
