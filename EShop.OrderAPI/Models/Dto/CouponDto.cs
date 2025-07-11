using System.ComponentModel.DataAnnotations;

namespace EShop.OrderAPI.Models.Dto
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
    }
}
