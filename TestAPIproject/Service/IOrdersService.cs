using TestAPIproject.Models;

namespace TestAPIproject.Service
{
    public interface IOrdersService
    {
        Task<Order?> AddOrderAsync(OrdersDto dto, int userId);
        Task<List<Order>> GetOrderByUserId(int userId);
        Task<Order> GetOrderByOrderId(int orderId);
        Task UpdateOrdersAsync(UpdateOrders updateOrders, int id);

        Task DeleteOrdersAsync(int orderId, int userId);
    }
}
