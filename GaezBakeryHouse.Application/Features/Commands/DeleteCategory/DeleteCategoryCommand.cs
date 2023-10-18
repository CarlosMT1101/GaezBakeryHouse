using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; private set; }

        public DeleteCategoryCommand(int id) =>
            Id = id;
    }
}
