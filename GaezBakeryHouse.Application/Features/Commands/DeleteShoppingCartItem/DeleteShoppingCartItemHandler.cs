using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemHandler : IRequestHandler<DeleteShoppingCartItemCommand>
    {
        readonly IMapper _mapper;
        readonly IShoppingCartItemRepository _repository;

        public DeleteShoppingCartItemHandler(IMapper mapper, 
                                             IShoppingCartItemRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Handle(DeleteShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingItem = await _repository.GetShoppingCartItemAsync(request.Id, request.ProductId, request.ApplicationUserId);

            await _repository.DeleteAsync(shoppingItem);
        }
    }
}
