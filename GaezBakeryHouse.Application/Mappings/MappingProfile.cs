using AutoMapper;
using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>();
        }
    }
}
