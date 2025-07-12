using AutoMapper;
using CouponAPI.Models;
using CouponAPI.Models.Dto;
using CouponAPI.Repository;

namespace CouponAPI.Services
{
    public class CouponService(ICouponRepository couponRepository, IMapper mapper) : ICouponService
    {
        private readonly ICouponRepository _couponRepository = couponRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<CouponDto> CreateCouponAsync(CouponDto couponDto)
        {
            couponDto.CouponCode = couponDto.CouponCode.ToLower();
            var existingCoupon = await _couponRepository.FindCouponByCodeAsync(couponDto.CouponCode);
            if (existingCoupon != null)
            {
                throw new InvalidOperationException($"Coupon code '{couponDto.CouponCode}' already exists.");
            }
            var coupon = _mapper.Map<Coupon>(couponDto);
            var createdCoupon = await _couponRepository.CreateCouponAsync(coupon);
            return _mapper.Map<CouponDto>(createdCoupon);
        }

        public async Task DeleteCouponAsync(int couponId)
        {
            await _couponRepository.DeleteCouponAsync(couponId);
        }

        public async Task<CouponDto> GetCouponByCodeAsync(string couponCode)
        {
            var coupon = await _couponRepository.GetCouponByCodeAsync(couponCode);
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<CouponDto> UpdateCouponAsync(int couponId, CouponDto couponDto)
        {
            couponDto.CouponCode = couponDto.CouponCode.ToLower();
            var coupon = _mapper.Map<Coupon>(couponDto);
            var updatedCoupon = await _couponRepository.UpdateCouponAsync(couponId, coupon);
            return _mapper.Map<CouponDto>(updatedCoupon);
        }

        public async Task<IEnumerable<CouponDto>> GetCouponsAsync()
        {
            var coupons = await _couponRepository.GetCouponsAsync();
            return _mapper.Map<IEnumerable<CouponDto>>(coupons);
        }
    }
}
