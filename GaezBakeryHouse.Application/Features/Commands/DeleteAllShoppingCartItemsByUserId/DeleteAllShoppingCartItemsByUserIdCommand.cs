using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.DeleteAllShoppingCartItemsByUserId
{
    public class DeleteAllShoppingCartItemsByUserIdCommand : IRequest
    {
        public string ApplicationUserId { get; private set; }

        public DeleteAllShoppingCartItemsByUserIdCommand(string userId) =>
            ApplicationUserId = userId;
    }
}
