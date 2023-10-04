using MediatR;


namespace GaezBakeryHouse.Application.Features.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { private set; get; }

        public DeleteProductCommand(int id) =>
            Id = id;
    }
}
