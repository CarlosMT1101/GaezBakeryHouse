using GaezBakeryHouse.Application.DTOs;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetShoppingCartItemsByUserId
{
    public class GetShoppingCartItemsByUserIdQuery : IRequest<IQueryable<ShoppingCartItemDTO>>
    {
        public string Id { get; private set; }

        public GetShoppingCartItemsByUserIdQuery(string id) =>
            Id = id;
    }
}
