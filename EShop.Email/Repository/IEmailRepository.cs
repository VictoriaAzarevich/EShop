using EShop.Contracts;

namespace EShop.Email.Repository
{
    public interface IEmailRepository
    {
        Task SendAndLogEmail(IUpdatePaymentResultMessage message);
    }
}
