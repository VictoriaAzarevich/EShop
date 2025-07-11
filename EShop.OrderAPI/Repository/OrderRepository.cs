using EShop.OrderAPI.DbContexts;
using EShop.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.OrderAPI.Repository
{
    public class OrderRepository(ApplicationDbContext dbContext, ILogger<OrderRepository> logger) : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ILogger<OrderRepository> _logger = logger;

        public async Task<bool> AddOrderAsync(OrderHeader orderHeader)
        {
            try
            {
                _dbContext.OrderHeaders.Add(orderHeader);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding order.");
                return false;
            }
        }

        public async Task<bool> UpdateOrderPaymentStatusAsync(int orderHeaderId, bool paid)
        {
            try
            {
                var orderHeaderFromDb = await _dbContext.OrderHeaders.FirstOrDefaultAsync(u => u.OrderHeaderId == orderHeaderId);
                if (orderHeaderFromDb == null)
                {
                    _logger.LogWarning("Order with ID {OrderHeaderId} not found.", orderHeaderId);
                    return false;
                }

                orderHeaderFromDb.PaymentStatus = paid;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating payment status for order {OrderHeaderId}.", orderHeaderId);
                return false;
            }
        }
    }
}
