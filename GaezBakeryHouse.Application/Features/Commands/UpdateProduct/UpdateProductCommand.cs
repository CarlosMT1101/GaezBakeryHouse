using MediatR;
using Microsoft.AspNetCore.Http;

namespace GaezBakeryHouse.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile ProductImage { get; set; }
        public decimal Price { get; set; }
        public bool IsTrendingProduct { get; set; }
        public int CategoryId { get; set; }
    }
}
