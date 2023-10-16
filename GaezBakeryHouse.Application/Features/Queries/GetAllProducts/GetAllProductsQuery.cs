using GaezBakeryHouse.Application.DTOs;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IQueryable<GetAllProductDTO>>
    {
    }
}
