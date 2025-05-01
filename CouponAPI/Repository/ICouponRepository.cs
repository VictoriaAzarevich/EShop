using CouponAPI.Models;

namespace CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCouponByCodeAsync(string couponCode);
        Task<Coupon> CreateCouponAsync(Coupon coupon);
        Task<Coupon> UpdateCouponAsync(int couponId, Coupon coupon);
        Task DeleteCouponAsync(int couponId);
        Task<IEnumerable<Coupon>> GetCouponsAsync();
    }
}
