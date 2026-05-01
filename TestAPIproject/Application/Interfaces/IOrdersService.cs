using TestAPIproject.Application.Dto;
using TestAPIproject.Domain;

namespace TestAPIproject.Application.Interfaces
{
    public interface IOrdersService
    {
        Task<Order?> AddOrderAsync(AddOrdersDto dto, int userId);
        Task<List<Order>> GetOrderByUserId(int userId);
        Task<Order> GetOrderByOrderId(int orderId);
        Task UpdateOrdersAsync(UpdateOrdersDto updateOrders, int id);

        Task DeleteOrdersAsync(int orderId, int userId);
    }
}
