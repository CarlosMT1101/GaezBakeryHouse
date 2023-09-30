using MediatR;


namespace GaezBakeryHouse.Application.Features.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { private set; get; }

        public DeleteProductCommand(int id) =>
            Id = id;
    }
}
