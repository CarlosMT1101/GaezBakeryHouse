using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaezBakeryHouse.Application.Features.Queries.GetOrderDetail
{
    public class GetOrderDetailHandler : IRequestHandler<GetOrderDetailQuery, IQueryable<OrderDetailDTO>>
    {
        readonly IOrderRepository _repository;
        readonly IMapper _mapper;

        public GetOrderDetailHandler(IOrderRepository repository,
                                     IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IQueryable<OrderDetailDTO>> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var orders = _repository.GetOrderDetail(request.OrderId);

            var orderDetailsDTO = from t1 in orders
                                  select new OrderDetailDTO
                                  {
                                      Id = t1.Id,
                                      ProductName = t1.Product.Name,
                                      ProductPrice = t1.Product.Price,
                                      Quantity = t1.Quantity,
                                      SubTotal = t1.TotalAmount
                                  };

            return Task.FromResult(orderDetailsDTO);
        }
    }
}
