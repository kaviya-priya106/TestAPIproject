using TestAPIproject.Application.Dto;
using TestAPIproject.Domain;

namespace TestAPIproject.Application.Interfaces
{
    public interface IOrdersService
    {
        Task<OrdersDto> AddOrderAsync(AddOrdersDto dto, int userId);
        Task<IEnumerable<OrdersDto>> GetOrderByUserId(int userId);
        Task<OrdersDto> GetOrderByOrderId(int orderId);
        Task UpdateOrdersAsync(UpdateOrdersDto updateOrders, int id);
        Task PatchOrderAsync(int orderId, int userId, PatchOrderDto dto);
        Task DeleteOrdersAsync(int orderId, int userId);
    }
}
