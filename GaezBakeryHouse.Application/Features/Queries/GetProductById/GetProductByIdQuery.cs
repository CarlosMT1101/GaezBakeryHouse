using GaezBakeryHouse.Application.DTOs;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetProductById
{
    public record GetProductByIdQuery : IRequest<ProductDTO>
    {
        public int ProductId { get; private set; }

        public GetProductByIdQuery(int productId)
        {
            ProductId = productId;       
        }
    }
}
