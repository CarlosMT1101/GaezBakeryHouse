using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Domain.Entities;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, IQueryable<ProductDTO>>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public GetProductsByCategoryHandler(IProductRepository repository, 
                                            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IQueryable<ProductDTO>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = _repository.GetProductsByCategory(request.CategoryId);
            var productsDTO = _mapper.ProjectTo<ProductDTO>(products);

            return Task.FromResult(productsDTO);
        }
    }
}
