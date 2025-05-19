using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Models.Dto;
using ShoppingCartAPI.Services;

namespace ShoppingCartAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService, ILogger<CartController> logger) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;
        private readonly ILogger<CartController> _logger = logger;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserId(string userId)
        {
            try
            {
                var cartDto = await _cartService.GetCartByUserIdAsync(userId);
                return Ok(cartDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Cart for user {UserId} not found.", userId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching cart for user {UserId}.", userId);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> CreateUpdateCart([FromBody] CartDto cartDto)
        {
            if (cartDto == null || cartDto.CartDetails.Count == 0)
                return BadRequest(new { message = "Cart cannot be empty" });

            var updatedCart = await _cartService.CreateUpdateCartAsync(cartDto);
            return Ok(updatedCart);
        }

        [HttpDelete("remove-item/{cartDetailsId}")]
        public async Task<IActionResult> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                await _cartService.RemoveFromCartAsync(cartDetailsId);
                return Ok(new { message = "Item removed from cart" });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Item with ID {cartDetailsId} not found for deletion.", cartDetailsId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing item from cart with ID {cartDetailsId}.", cartDetailsId);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("apply-coupon")]
        public async Task<IActionResult> ApplyCoupon([FromBody] CartDto cartDto)
        {
            if (string.IsNullOrEmpty(cartDto.CartHeader.UserId) || string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                return BadRequest(new { message = "UserId and CouponCode are required" });

            await _cartService.ApplyCouponAsync(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);
            return Ok(new { message = "Coupon applied successfully" });
        }

        [HttpPost("remove-coupon/{userId}")]
        public async Task<IActionResult> RemoveCoupon(string userId)
        {
            await _cartService.RemoveCouponAsync(userId);
            return Ok(new { message = "Coupon removed successfully" });
        }
    }
}
