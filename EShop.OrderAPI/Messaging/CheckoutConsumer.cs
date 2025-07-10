using EShop.Contracts;
using EShop.OrderAPI.Models;
using EShop.OrderAPI.Repository;
using MassTransit;

namespace EShop.OrderAPI.Messaging
{
    public class CheckoutConsumer(IOrderRepository orderRepository, ILogger<CheckoutConsumer> logger) : IConsumer<ICheckoutHeader>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ILogger<CheckoutConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<ICheckoutHeader> context)
        {
            var message = context.Message;

            try
            {
                var orderHeader = new OrderHeader
                {
                    UserId = message.UserId,
                    CouponCode = message.CouponCode,
                    OrderTotal = message.OrderTotal,
                    DiscountTotal = message.DiscountTotal,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    PickupDateTime = DateTime.SpecifyKind(message.PickupDateTime, DateTimeKind.Utc),
                    Phone = message.Phone,
                    Email = message.Email,
                    CardNumber = message.CardNumber,
                    CVV = message.CVV,
                    ExpiryMonthYear = message.ExpiryMonthYear,
                    CartTotalItems = message.CartTotalItems ?? 0,
                    OrderTime = DateTime.UtcNow,
                    PaymentStatus = false,
                    OrderDetails = message.CartDetails.Select(cd => new OrderDetails
                    {
                        ProductId = cd.ProductId,
                        Count = cd.Count,
                        ProductName = cd.ProductName,
                        Price = cd.Price
                    }).ToList()
                };

                await _orderRepository.AddOrder(orderHeader);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing order message for user {UserId}", message.UserId);
                throw;
            }
        }
    }
}
