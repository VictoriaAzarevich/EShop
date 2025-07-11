using EShop.OrderAPI.Models;

namespace EShop.OrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrderAsync(OrderHeader orderHeader);
        Task<bool> UpdateOrderPaymentStatusAsync(int orderHeaderId, bool paid);
    }
}
