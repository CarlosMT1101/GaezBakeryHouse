using GaezBakeryHouse.Application.Contracts;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.DeleteAllShoppingCartItemsByUserId
{
    public class DeleteAllShoppingCartItemsByUserIdHandler : IRequestHandler<DeleteAllShoppingCartItemsByUserIdCommand>
    {
        readonly IShoppingCartItemRepository _repository;

        public DeleteAllShoppingCartItemsByUserIdHandler(IShoppingCartItemRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteAllShoppingCartItemsByUserIdCommand request, CancellationToken cancellationToken) =>
            await _repository.DeleteAllShoppingCartItemsByUserId(request.ApplicationUserId);
    }
}
