using AutoMapper;
using GaezBakeryHouse.Application.Contracts;
using GaezBakeryHouse.Application.DTOs;
using MediatR;

namespace GaezBakeryHouse.Application.Features.Queries.GetShoppingCartItemsByUserId
{
    public class GetShoppingCartItemsByUserIdHandler : IRequestHandler<GetShoppingCartItemsByUserIdQuery, IQueryable<ShoppingCartItemDTO>>
    {
        readonly IMapper _mapper;
        readonly IShoppingCartItemRepository _repository;

        public GetShoppingCartItemsByUserIdHandler(IMapper mapper, 
                                                   IShoppingCartItemRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task<IQueryable<ShoppingCartItemDTO>> Handle(GetShoppingCartItemsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var shopItems = _repository.GetShoppingCartItemsByUserId(request.Id);

            var shopItemsDTO = _mapper.ProjectTo<ShoppingCartItemDTO>(shopItems);

            return Task.FromResult(shopItemsDTO);
        }
    }
}
