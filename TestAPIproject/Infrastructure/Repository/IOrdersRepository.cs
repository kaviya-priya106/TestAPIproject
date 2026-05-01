using TestAPIproject.Domain;

namespace TestAPIproject.Infrastructure.Repository
{
    public interface IOrdersRepository
    {
        Task AddOrderAsync(Order order);
        Task<List<Order?>> GetOrderByUserId(int userId);
        Task<Order> GetOrderByOrderId(int orderId);
        Task UpdateOrdersAsync();

        Task DeleteOrdersAsync(int id);
    }
}
