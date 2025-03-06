using ProductAPI.Models.Dto;
using ProductAPI.Models;
using AutoMapper;

namespace ProductAPI
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
