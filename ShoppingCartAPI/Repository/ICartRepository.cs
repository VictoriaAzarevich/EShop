using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task<Cart> CreateUpdateCartAsync(Cart cart);
        Task RemoveFromCartAsync(int cartDetailsId);
        Task ApplyCouponAsync(string userId, string couponCode);
        Task RemoveCouponAsync(string userId);
        Task ClearCartAsync(string userId);
    }
}
