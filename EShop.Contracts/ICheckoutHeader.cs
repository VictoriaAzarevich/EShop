using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Contracts
{
    public interface ICheckoutHeader : IBaseMessage
    {
        int CartHeaderId { get; }
        string UserId { get; }
        string CouponCode { get; }
        double OrderTotal { get; }
        double DiscountTotal { get; }
        string FirstName { get; }
        string LastName { get; }
        DateTime PickupDateTime { get; }
        string Phone { get; }
        string Email { get; }
        string CardNumber { get; }
        string CVV { get; }
        string ExpiryMonthYear { get; }
        int? CartTotalItems { get; }
        IEnumerable<CartDetailsDto> CartDetails { get; }
    }
}
