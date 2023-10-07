using MediatR;

namespace GaezBakeryHouse.Application.Features.Commands.PostOrder
{
    public class PostOrderCommand : IRequest
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal OrderTotal { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
