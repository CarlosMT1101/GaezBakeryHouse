using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Domain.Entities;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.PostShoppingCartItem
{
    public class PostShoppinCartItemHandler : IRequestHandler<PostShoppingCartItemCommand>
    {
        readonly IMapper _mapper;
        readonly IShoppingCartItemRepository _repository;

        public PostShoppinCartItemHandler(IMapper mapper, 
                                          IShoppingCartItemRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Handle(PostShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var shopItem = _mapper.Map<PostShoppingCartItemCommand, ShoppingCartItem>(request);
            shopItem.TotalAmount = request.Price * request.Quantity;

            await _repository.PostAsync(shopItem);
        }
    }
}
