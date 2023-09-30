using MediatR;
using Microsoft.AspNetCore.Http;

namespace GaezBakeryHouse.Application.Features.Commands.PostCategoryCommand
{
    public class PostCategoryCommand : IRequest
    {
        public string Name { get; set; }
        public IFormFile CategoryImage { get; set; }
    }
}
