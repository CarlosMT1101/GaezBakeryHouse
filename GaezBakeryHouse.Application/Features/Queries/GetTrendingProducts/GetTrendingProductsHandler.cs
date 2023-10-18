using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Application.DTOs;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetTrendingProducts
{
    public class GetTrendingProductsHandler : IRequestHandler<GetTrendingProductsQuery, IQueryable<ProductDTO>>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public GetTrendingProductsHandler(IProductRepository repository,
                                          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IQueryable<ProductDTO>> Handle(GetTrendingProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _repository.GetTrendingProducts();
            var productsDTO = _mapper.ProjectTo<ProductDTO>(products);

            return Task.FromResult(productsDTO);
        }
    }
}
