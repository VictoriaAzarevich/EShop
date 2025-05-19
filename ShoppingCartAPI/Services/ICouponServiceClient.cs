using ShoppingCartAPI.Models.Dto;

namespace ShoppingCartAPI.Services
{
    public interface ICouponServiceClient
    {
        Task<CouponDto?> GetCouponByCodeAsync(string code);
    }
}
