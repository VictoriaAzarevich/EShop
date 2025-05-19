using CouponAPI.Models.Dto;
using CouponAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController(ICouponService couponService, ILogger<CouponController> logger) : ControllerBase
    {
        private readonly ICouponService _couponService = couponService;
        private readonly ILogger<CouponController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetCoupons()
        {
            try
            {
                var couponDtos = await _couponService.GetCouponsAsync();
                return Ok(couponDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching categories.");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{couponCode}")]
        public async Task<IActionResult> GetCouponByCode(string couponCode)
        {
            try
            {
                var coupon = await _couponService.GetCouponByCodeAsync(couponCode);
                return Ok(coupon);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Coupon with coupon code {couponCode} not found.", couponCode);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching coupon with coupon code {couponCode}.", couponCode);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<CouponDto>> CreateCupon([FromBody] CouponDto couponDto)
        {
            if (couponDto == null)
                return BadRequest(new { message = "Coupon cannot be empty" });

            var createdCoupon = await _couponService.CreateCouponAsync(couponDto);
            return Ok(createdCoupon);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{couponId}")]
        public async Task<ActionResult<CouponDto>> UpdateCupon(int couponId, [FromBody] CouponDto couponDto)
        {
            try
            {
                if (couponDto == null)
                    return BadRequest(new { message = "Coupon cannot be empty" });

                var updatedCoupon = await _couponService.UpdateCouponAsync(couponId, couponDto);
                return Ok(updatedCoupon);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Coupon with ID {couponId} not found for update.", couponId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating coupon with ID {couponId}.", couponId);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{couponId}")]
        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            try
            {
                await _couponService.DeleteCouponAsync(couponId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Coupon with ID {couponId} not found for deletion.", couponId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting coupon with ID {couponId}.", couponId);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
