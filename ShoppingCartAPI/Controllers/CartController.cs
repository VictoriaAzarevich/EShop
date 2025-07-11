using EShop.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Models.Dto;
using ShoppingCartAPI.Services;

namespace ShoppingCartAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService, ILogger<CartController> logger,
        IPublishEndpoint publishEndpoint, ICouponService couponService) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;
        private readonly ILogger<CartController> _logger = logger;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly ICouponService _couponService = couponService;

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

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(CheckoutHeaderDto checkoutHeaderDto)
        {
            try
            {
                CartDto cartDto = await _cartService.GetCartByUserIdAsync(checkoutHeaderDto.UserId);

                if (cartDto == null)
                {
                    return BadRequest();
                }

                if (!string.IsNullOrEmpty(checkoutHeaderDto.CouponCode))
                {
                    CouponDto couponDto = await _couponService.GetCouponByCodeAsync(checkoutHeaderDto.CouponCode);
                    
                    if (couponDto == null) 
                    {
                        return BadRequest(new { message = "Coupon is invalid or expired" });
                    }

                    if (couponDto.DiscountAmount != checkoutHeaderDto.DiscountTotal)
                    {
                        return BadRequest(new { message = "Coupon value has changed", correctValue = couponDto.DiscountAmount });
                    }
                }

                checkoutHeaderDto.CartDetails = cartDto.CartDetails;

                await _publishEndpoint.Publish<ICheckoutHeader>(new
                {
                    Id = Guid.NewGuid(),
                    MessageCreated = DateTime.UtcNow,
                    checkoutHeaderDto.CartHeaderId,
                    checkoutHeaderDto.UserId,
                    checkoutHeaderDto.CouponCode,
                    checkoutHeaderDto.OrderTotal,
                    checkoutHeaderDto.DiscountTotal,
                    checkoutHeaderDto.FirstName,
                    checkoutHeaderDto.LastName,
                    checkoutHeaderDto.PickupDateTime,
                    checkoutHeaderDto.Phone,
                    checkoutHeaderDto.Email,
                    checkoutHeaderDto.CardNumber,
                    checkoutHeaderDto.CVV,
                    checkoutHeaderDto.ExpiryMonthYear,
                    checkoutHeaderDto.CartTotalItems,
                    CartDetails = checkoutHeaderDto.CartDetails.Select(d => new
                    {
                        d.ProductId,
                        d.Count,
                        ProductName = d.Product?.Name ?? "",
                        Price = d.Product?.Price ?? 0
                    }).ToList()
                });

                await _cartService.ClearCartAsync(checkoutHeaderDto.UserId);
                return Ok(new { message = "The order has been placed" });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Cart for user {UserId} not found.", checkoutHeaderDto.UserId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while making an order for user {UserId}.", checkoutHeaderDto.UserId);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
