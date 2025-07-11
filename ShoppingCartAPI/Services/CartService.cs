using AutoMapper;
using ShoppingCartAPI.Models;
using ShoppingCartAPI.Models.Dto;
using ShoppingCartAPI.Repository;

namespace ShoppingCartAPI.Services
{
    public class CartService(ICartRepository cartRepository, ICouponService couponServiceClient, IMapper mapper) : ICartService
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly ICouponService _couponService = couponServiceClient;
        private readonly IMapper _mapper = mapper;

        public async Task ApplyCouponAsync(string userId, string couponCode)
        {
            await _cartRepository.ApplyCouponAsync(userId, couponCode);
        }

        public async Task ClearCartAsync(string userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }

        public async Task<CartDto> CreateUpdateCartAsync(CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            var createdCart = await _cartRepository.CreateUpdateCartAsync(cart);
            return _mapper.Map<CartDto>(createdCart);
        }

        public async Task<CartDto> GetCartByUserIdAsync(string userId)
        {
            var userCart = await _cartRepository.GetCartByUserIdAsync(userId);
            var cartDto = _mapper.Map<CartDto>(userCart);

            if (cartDto.CartHeader.CouponCode != null)
            {
                var coupon = await _couponService.GetCouponByCodeAsync(cartDto.CartHeader.CouponCode);

                if (coupon != null)
                {
                    cartDto.CartHeader.DiscountTotal = coupon.DiscountAmount;
                }
            }

            foreach (var detail in cartDto.CartDetails)
            {
                cartDto.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
            }

            cartDto.CartHeader.OrderTotal -= cartDto.CartHeader.DiscountTotal;

            return cartDto;
        }

        public async Task RemoveCouponAsync(string userId)
        {
            await _cartRepository.RemoveCouponAsync(userId);
        }

        public async Task RemoveFromCartAsync(int cartDetailsId)
        {
            await _cartRepository.RemoveFromCartAsync(cartDetailsId);
        }
    }
}
