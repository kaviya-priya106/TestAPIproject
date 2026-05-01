using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using TestAPIproject.Models;
using TestAPIproject.Repository;
using TestAPIproject.Dto;

namespace TestAPIproject.Service
{
    public class OrdersService:IOrdersService
    {

        private readonly IOrdersRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IMemoryCache _cache;
        public OrdersService(IOrdersRepository repo, IMapper mapper, ILogger<EmployeeService> logger, IMemoryCache cache)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }


        public async Task<List<Order?>> GetOrderByUserId(int id)
        {
            string cacheKey = $"orders_user_{id}";

            // Try get from cache
            if (!_cache.TryGetValue(cacheKey, out List<Order> orders))
            {
                // Not in cache → fetch from DB
                orders =  await _repo.GetOrderByUserId(id);

                // Store in cache
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)) // expires in 5 mins
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2)); // resets if accessed

                _cache.Set(cacheKey, orders, cacheOptions);
            }

            return orders;
            //return await _repo.GetOrderByUserId(id);
        }

        public async Task<Order?> GetOrderByOrderId(int orderid)
        {
            return await _repo.GetOrderByOrderId(orderid);
        }

        public async Task<Order> AddOrderAsync(AddOrdersDto dto, int userId)
        {
            var order = new Order(
                userId,
                dto.ProductName,
                dto.Quantity,
                dto.Price
            );

            await _repo.AddOrderAsync(order);

            _cache.Remove($"orders_user_{order.UserId}");

            _logger.LogInformation("Order created: {Product}", order.ProductName);

            return order;
        }

        /*public async Task<Order> AddOrderAsync(OrdersDto dto, int userId)
        {

            var orders = _mapper.Map<Order>(dto);
            orders.UserId = userId;
            await _repo.AddOrderAsync(orders);
            _cache.Remove($"orders_user_{orders.UserId}");
            _logger.LogInformation("Order created: {Product}", orders.ProductName);
            return orders;


        }*/

        /*public async Task UpdateOrdersAsync(UpdateOrders updateOrders, int id)
        {
            var order = await _repo.GetOrderByOrderId(updateOrders.Id);

            if (order == null)
                throw new Exception("Order not found");
            if (order.Id != id)
                throw new Exception("Unauthorized");

            order.ProductName = updateOrders.productName;
            order.Price = updateOrders.price;
            await _repo.UpdateOrdersAsync();
            _cache.Remove($"orders_user_{order.UserId}");

        }*/

        public async Task UpdateOrdersAsync(UpdateOrdersDto dto, int id)
        {
            var order = await _repo.GetOrderByOrderId(dto.Id);

            if (order == null)
                throw new Exception("Order not found");

            if (order.Id != id)
                throw new Exception("Unauthorized");

            order.SetProductName(dto.ProductName);
            order.SetQuantity(dto.Quantity);
            order.SetPrice(dto.Price);

            await _repo.UpdateOrdersAsync();

            _cache.Remove($"orders_user_{order.UserId}");
        }


        public async Task DeleteOrdersAsync(int orderId, int userId)
        {
            await _repo.DeleteOrdersAsync(orderId);
            _cache.Remove($"orders_user_{userId}");
        }
    }
}
