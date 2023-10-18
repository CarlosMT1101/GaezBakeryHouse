using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaezBakeryHouse.Application.Features.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IQueryable<GetAllProductDTO>>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public GetAllProductsHandler(IProductRepository repository,
                                     IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IQueryable<GetAllProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _repository.GetAll();
            var productsDTO = _mapper.ProjectTo<GetAllProductDTO>(products);

            return Task.FromResult(productsDTO);
        }
    }
}
