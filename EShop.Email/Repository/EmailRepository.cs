using EShop.Contracts;
using EShop.Email.DbContexts;
using EShop.Email.Models;

namespace EShop.Email.Repository
{
    public class EmailRepository(ApplicationDbContext dbContext) : IEmailRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task SendAndLogEmail(IUpdatePaymentResultMessage message)
        {
            // Implement an email sender
            EmailLog emailLog = new EmailLog()
            {
                Email = message.Email,
                EmailSent = DateTime.Now,
                Log = $"Order - {message.OrderId} has been created successfully."
            };

            _dbContext.Add(emailLog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
