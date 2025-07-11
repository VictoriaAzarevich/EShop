using ShoppingCartAPI.Models.Dto;

namespace ShoppingCartAPI.Services
{
    public interface ICouponService
    {
        Task<CouponDto?> GetCouponByCodeAsync(string code);
    }
}
