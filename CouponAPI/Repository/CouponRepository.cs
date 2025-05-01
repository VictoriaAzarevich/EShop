using CouponAPI.DBContext;
using CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponAPI.Repository
{
    public class CouponRepository(ApplicationDbContext dbContext) : ICouponRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Coupon> CreateCouponAsync(Coupon coupon)
        {
            _dbContext.Add(coupon);
            await _dbContext.SaveChangesAsync();
            return coupon;
        }

        public async Task DeleteCouponAsync(int couponId)
        {
            var coupon = await _dbContext.Coupons
                .FirstOrDefaultAsync(c => c.CouponId == couponId);

            if (coupon == null)
            {
                throw new KeyNotFoundException("Coupon not found");
            }

            _dbContext.Coupons.Remove(coupon);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Coupon> GetCouponByCodeAsync(string couponCode)
        {
            var coupon = await _dbContext.Coupons
                .FirstOrDefaultAsync(c => c.CouponCode == couponCode);

            if (coupon == null)
            {
                throw new KeyNotFoundException("Coupon not found");
            }

            return coupon;
        }

        public async Task<Coupon> UpdateCouponAsync(int couponId, Coupon updatedCoupon)
        {
            var coupon = await _dbContext.Coupons
                .FirstOrDefaultAsync(c => c.CouponId == couponId);

            if (coupon == null)
            {
                throw new KeyNotFoundException("Coupon not found");
            }

            coupon.CouponCode = updatedCoupon.CouponCode;
            coupon.DiscountAmount = updatedCoupon.DiscountAmount;
            await _dbContext.SaveChangesAsync();
            return coupon;
        }

        public async Task<IEnumerable<Coupon>> GetCouponsAsync()
        {
            var coupons = await _dbContext.Coupons.ToListAsync();
            return coupons;
        }
    }
}
