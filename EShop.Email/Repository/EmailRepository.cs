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
            if (string.IsNullOrWhiteSpace(message.Email))
            {
                throw new ArgumentException("Email address is required", nameof(message.Email));
            }

            Console.WriteLine($"Simulated email to {message.Email}: Order {message.OrderId} has been created.");

            EmailLog emailLog = new EmailLog()
            {
                Email = message.Email,
                EmailSent = DateTime.UtcNow,
                Log = $"Order - {message.OrderId} has been created successfully."
            };

            _dbContext.Add(emailLog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
