using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetUserTotalAmount
{
    public class GetUserTotalAmountQuery : IRequest<decimal>
    {
        public string ApplicationUserId { get; private set; }

        public GetUserTotalAmountQuery(string userId) => 
            ApplicationUserId = userId;
    }
}
