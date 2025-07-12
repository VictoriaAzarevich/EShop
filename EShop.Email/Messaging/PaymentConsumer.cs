using EShop.Contracts;
using EShop.Email.Repository;
using MassTransit;

namespace EShop.Email.Messaging
{
    public class PaymentConsumer(IEmailRepository emailRepository, ILogger<PaymentConsumer> logger) : IConsumer<IUpdatePaymentResultMessage>
    {
        private readonly IEmailRepository _emailRepository = emailRepository;
        private readonly ILogger<PaymentConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<IUpdatePaymentResultMessage> context)
        {
            var message = context.Message;

            try
            {
                await _emailRepository.SendAndLogEmail(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email for payment update message with Id: {MessageId}", message.Id);
            }
        }
    }
}