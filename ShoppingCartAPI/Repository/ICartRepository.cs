using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserId(string userId);
        Task<Cart> CreateUpdateCart(Cart cart);
        Task RemoveFromCart(int cartDetailsId);
        Task ApplyCoupon(string userId, string couponCode);
        Task RemoveCoupon(string userId);
        Task ClearCart(string userId);
    }
}
