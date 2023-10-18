using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Application.DTOs;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetOrdersByUser
{
    public class GetOrdersByUserHandler : IRequestHandler<GetOrdersByUserQuery, IQueryable<OrderDTO>>
    {
        readonly IOrderRepository _repository;
        readonly IMapper _mapper;

        public GetOrdersByUserHandler(IOrderRepository repository,
                                     IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IQueryable<OrderDTO>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            var orders = _repository.GetOrdersByUser(request.Id);
            var ordersDTO = _mapper.ProjectTo<OrderDTO>(orders);

            return Task.FromResult(ordersDTO);
        }
    }
}
