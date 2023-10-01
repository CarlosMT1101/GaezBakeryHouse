using AutoMapper;
using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Features.Commands.PostCategoryCommand;
using GaezBakeryHouse.Application.Features.Commands.PostProductCommand;
using GaezBakeryHouse.Application.Features.Commands.UpdateCategoryCommand;
using GaezBakeryHouse.Application.Features.Commands.UpdateProductCommand;
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
                .ForMember(x => x.ProductImage, x => x.Ignore()); ;
        }
    }
}
