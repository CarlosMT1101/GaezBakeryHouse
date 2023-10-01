using GaezBakeryHouse.Application.DTOs;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetProductsByCategory
{
    public record GetProductsByCategoryQuery : IRequest<IQueryable<ProductDTO>>
    {
        public int CategoryId { get; private set; }

        public GetProductsByCategoryQuery(int categoryId) =>
            CategoryId = categoryId;
    }
}
