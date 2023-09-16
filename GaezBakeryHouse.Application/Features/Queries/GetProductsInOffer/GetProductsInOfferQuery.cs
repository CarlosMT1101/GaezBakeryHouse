using GaezBakeryHouse.Application.DTOs;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetProductsInOffer
{
    public class GetProductsInOfferQuery : IRequest<IEnumerable<ProductDTO>>
    {
    }
}
