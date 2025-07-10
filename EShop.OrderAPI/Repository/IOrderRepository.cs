using EShop.OrderAPI.Models;

namespace EShop.OrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader orderHeader);
        Task<bool> UpdateOrderPaymentStatus(int orderHeaderId, bool paid);
    }
}
