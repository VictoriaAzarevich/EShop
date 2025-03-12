using ShoppingCartAPI.Models.Dto;

namespace ShoppingCartAPI.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartByUserIdAsync(string userId);
        Task<CartDto> CreateUpdateCartAsync(CartDto cartDto);
        Task RemoveFromCartAsync(int cartDetailsId);
        Task ApplyCouponAsync(string userId, string couponCode);
        Task RemoveCouponAsync(string userId);
        Task ClearCartAsync(string userId);
    }
}