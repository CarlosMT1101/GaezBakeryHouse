using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.PostOrder
{
    public class PostOrderHandler : IRequestHandler<PostOrderCommand>
    {
        readonly IOrderRepository _repository;
        readonly IMapper _mapper;

        public PostOrderHandler(IOrderRepository repository, 
                                IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<PostOrderCommand, Order>(request);
            order.IsOrderCompleted = false;
            order.OrderPlaced = DateTime.Now;

            await _repository.PostAsync(order);
        }
    }
}
