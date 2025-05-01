using CouponAPI.Models.Dto;

namespace CouponAPI.Services
{
    public interface ICouponService
    {
        Task<CouponDto> GetCouponByCodeAsync(string couponCode);
        Task<CouponDto> CreateCouponAsync(CouponDto couponDto);
        Task<CouponDto> UpdateCouponAsync(int couponId, CouponDto couponDto);
        Task DeleteCouponAsync(int couponId);
        Task<IEnumerable<CouponDto>> GetCouponsAsync();
    }
}
