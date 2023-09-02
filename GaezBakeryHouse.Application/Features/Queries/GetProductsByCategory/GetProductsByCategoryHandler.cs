using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Domain.Entities;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, IEnumerable<ProductDTO>>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public GetProductsByCategoryHandler(IProductRepository repository,
                                            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
            

        public async Task<IEnumerable<ProductDTO>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var productsList = await _repository.GetProductsByCategory(request.CategoryId);

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(productsList);
        }
    }
}
