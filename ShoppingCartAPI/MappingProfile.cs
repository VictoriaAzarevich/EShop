using AutoMapper;
using ShoppingCartAPI.Models;
using ShoppingCartAPI.Models.Dto;

namespace ShoppingCartAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
