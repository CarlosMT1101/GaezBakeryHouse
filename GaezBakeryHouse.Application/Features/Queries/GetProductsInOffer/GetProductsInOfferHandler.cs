using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Domain.Entities;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetProductsInOffer
{
    public class GetProductsInOfferHandler : IRequestHandler<GetProductsInOfferQuery, IEnumerable<ProductDTO>>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public GetProductsInOfferHandler(IProductRepository repository,
                                            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetProductsInOfferQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetProductsInOffer();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }
    }
}
