using AutoMapper;
using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Features.Commands.PostCategory;
using GaezBakeryHouse.Application.Features.Commands.PostProduct;
using GaezBakeryHouse.Application.Features.Commands.PostShoppingCartItem;
using GaezBakeryHouse.Application.Features.Commands.UpdateCategory;
using GaezBakeryHouse.Application.Features.Commands.UpdateProduct;
using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<PostProductCommand, Product>()
                .ForMember(x => x.ProductImage, x => x.Ignore());

            CreateMap<PostCategoryCommand, Category>()
                .ForMember(x => x.CategoryImage, x => x.Ignore());

            CreateMap<UpdateCategoryCommand, Category>()
                .ForMember(x => x.CategoryImage, x => x.Ignore());

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(x => x.ProductImage, x => x.Ignore());

            CreateMap<PostShoppingCartItemCommand, ShoppingCartItem>();

            CreateMap<ShoppingCartItem, ShoppingCartItemDTO>().ReverseMap();
        }
    }
}
