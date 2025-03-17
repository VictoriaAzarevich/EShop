using AutoMapper;
using CouponAPI.Models;
using CouponAPI.Models.Dto;

namespace CouponAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CouponDto, CouponDto>().ReverseMap();
        }
    }
}
