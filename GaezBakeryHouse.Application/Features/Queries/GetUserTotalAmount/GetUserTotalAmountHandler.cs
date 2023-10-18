using GaezBakeryHouse.Application.Contracts;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetUserTotalAmount
{
    public class GetUserTotalAmountHandler : IRequestHandler<GetUserTotalAmountQuery, decimal>
    {
        readonly IShoppingCartItemRepository _repository;

        public GetUserTotalAmountHandler(IShoppingCartItemRepository repository) =>
            _repository = repository;

        Task<decimal> IRequestHandler<GetUserTotalAmountQuery, decimal>.Handle(GetUserTotalAmountQuery request, CancellationToken cancellationToken) =>
             _repository.GetUserTotalAmount(request.ApplicationUserId);
    }
}
