using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GaezBakeryHouse.Application.Features.Commands.PostShoppingCartItem
{ 
    public class PostShoppingCartItemCommand : IRequest
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
    }
}
