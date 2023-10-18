using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemCommand : IRequest
    {
        public int Id { get; private set; }
        public int ProductId { get; set; }
        public string ApplicationUserId { get; private set; }

        public DeleteShoppingCartItemCommand(int id, int productId, string applicationUserId)
        {
            Id = id;
            ProductId = productId;
            ApplicationUserId = applicationUserId;
        }
    }
}
