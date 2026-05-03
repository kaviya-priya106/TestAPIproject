using EmployeeOrderManagementAPI.Application.Dto;
using EmployeeOrderManagementAPI.Domain;

namespace EmployeeOrderManagementAPI.Application.Interfaces
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
