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

            var shopItemsListDTO = from t1 in shopItems
                                   select new ShoppingCartItemDTO
                                   {
                                        ApplicationUserId = t1.ApplicationUserId,
                                        Id = t1.Id,
                                        Price = t1.Price,
                                        ProductId = t1.ProductId,
                                        ProductImage = t1.Product.ProductImage,
                                        Quantity = t1.Quantity,
                                        TotalAmount = t1.TotalAmount,
                                        ProductName = t1.Product.Name
                                   };

            return Task.FromResult(shopItemsListDTO);
        }
    }
}
