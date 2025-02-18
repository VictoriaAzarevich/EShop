using AutoMapper;
using EShop.Services.ProductAPI.Models;
using EShop.Services.ProductAPI.Models.Dto;

namespace EShop.Services.ProductAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(scr => scr.Category.CategoryName));
            CreateMap<ProductCreateUpdateDto, Product>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateUpdateDto, Category>();
        }
    }
}
