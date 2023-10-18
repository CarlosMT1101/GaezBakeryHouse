using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Domain.Entities;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public GetProductByIdQuery(int id) =>
            Id = id;

        public int Id { get; private set; }
    }
}
