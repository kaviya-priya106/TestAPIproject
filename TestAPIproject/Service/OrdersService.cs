using AutoMapper;
using TestAPIproject.Models;
using TestAPIproject.Repository;

namespace TestAPIproject.Service
{
    public class OrdersService:IOrdersService
    {

        private readonly IOrdersRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;
        public OrdersService(IOrdersRepository repo, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<List<Order?>> GetOrderByUserId(int id)
        {
            return await _repo.GetOrderByUserId(id);
        }

        public async Task<Order?> GetOrderByOrderId(int orderid)
        {
            return await _repo.GetOrderByOrderId(orderid);
        }

        public async Task<Order> AddOrderAsync(OrdersDto dto, int userId)
        {

            var orders = _mapper.Map<Order>(dto);
            orders.UserId = userId;
            await _repo.AddOrderAsync(orders);
            _logger.LogInformation("Order created: {Product}", orders.ProductName);
            return orders;


        }

        public async Task UpdateOrdersAsync(UpdateOrders updateOrders, int id)
        {
            var order = await _repo.GetOrderByOrderId(updateOrders.Id);

            if (order == null)
                throw new Exception("Order not found");
            if (order.Id != id)
                throw new Exception("Unauthorized");

            order.ProductName = updateOrders.productName;
            order.Price = updateOrders.price;
            await _repo.UpdateOrdersAsync();


        }


        public async Task DeleteOrdersAsync(int id)
        {
            await _repo.DeleteOrdersAsync(id);
        }
    }
}
