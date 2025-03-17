namespace CouponAPI.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public required string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
    }
}
