using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Domain.Entities;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        readonly IProductRepository _repository;

        public GetProductByIdHandler(IMapper mapper,
                                     IProductRepository repository)
        {
            _repository = repository;
        }


        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductById(request.ProductId);

            return new ProductDTO
            {
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Id = product.Id,
                Image = product.Image
            };
        }
    }
}
