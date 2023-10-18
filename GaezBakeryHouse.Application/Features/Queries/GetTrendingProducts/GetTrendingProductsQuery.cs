using GaezBakeryHouse.Application.DTOs;
using MediatR;
using System;
namespace GaezBakeryHouse.Application.Features.Queries.GetTrendingProducts
{
    public class GetTrendingProductsQuery : IRequest<IQueryable<ProductDTO>>
    {
    }
}
