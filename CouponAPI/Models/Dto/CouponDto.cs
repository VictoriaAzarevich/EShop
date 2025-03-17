using System.ComponentModel.DataAnnotations;

namespace CouponAPI.Models.Dto
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
    }
}
