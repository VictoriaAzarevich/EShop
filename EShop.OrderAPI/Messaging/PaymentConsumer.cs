using EShop.Contracts;
using EShop.OrderAPI.Repository;
using MassTransit;

namespace EShop.OrderAPI.Messaging
{
    public class PaymentConsumer(IOrderRepository orderRepository) : IConsumer<IUpdatePaymentResultMessage>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task Consume(ConsumeContext<IUpdatePaymentResultMessage> context)
        {
            var message = context.Message;

            await _orderRepository.UpdateOrderPaymentStatusAsync(message.OrderId, message.Status);
        }
    }
}
