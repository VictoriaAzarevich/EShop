using EShop.Contracts;
using MassTransit;
using PaymentProcessor;

namespace EShop.PaymentAPI.Messaging
{
    public class PaymentConsumer(IProcessPayment processPayment, IPublishEndpoint publishEndpoint) : IConsumer<IPaymentRequestMessage>
    {
        private readonly IProcessPayment _processPayment = processPayment;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<IPaymentRequestMessage> context)
        {
            var message = context.Message;

            var result = _processPayment.PaymentProcessor();

            await _publishEndpoint.Publish<IUpdatePaymentResultMessage>(new
            {
                Id = Guid.NewGuid(),
                MessageCreated = DateTime.UtcNow,
                message.OrderId,
                Status = result,
                message.Email
            });
        }
    }
}
