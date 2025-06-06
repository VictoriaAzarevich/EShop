﻿namespace ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public  double OrderTotal { get; set; }
        public double DiscountTotal { get; set; }
    }
}
