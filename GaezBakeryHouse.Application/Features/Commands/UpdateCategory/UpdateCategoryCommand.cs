using MediatR;
using Microsoft.AspNetCore.Http;

namespace GaezBakeryHouse.Application.Features.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile CategoryImage { get; set; }
    }
}
