using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using TestAPIproject.Application.Dto;
using TestAPIproject.Infrastructure.Repository;
using TestAPIproject.Application.Interfaces;
using TestAPIproject.Domain;

namespace TestAPIproject.Application.Service
{
    public class OrdersService : IOrdersService
    {

        private readonly IOrdersRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersService> _logger;
        private readonly IMemoryCache _cache;

        public OrdersService(IOrdersRepository repo, IMapper mapper, ILogger<OrdersService> logger, IMemoryCache cache)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }


        public async Task<IEnumerable<OrdersDto>> GetOrderByUserId(int id)
        {
            string cacheKey = $"orders_user_{id}";

            if (!_cache.TryGetValue(cacheKey, out List<Order> orders))
            {
                orders = await _repo.GetOrderByUserId(id);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                _cache.Set(cacheKey, orders, cacheOptions);
            }

            return orders.Select(o => new OrdersDto
            {
                Id = o.Id,
                ProductName = o.ProductName,
                Quantity = o.Quantity,
                Price = o.Price,
                TotalAmount = o.Quantity * o.Price
            }).ToList();
        }

        public async Task<OrdersDto> GetOrderByOrderId(int orderid)
        {
            var order = await _repo.GetOrderByOrderId(orderid);

            if (order == null)
                return null;

            return new OrdersDto
            {
                Id = order.Id,
                ProductName = order.ProductName,
                Quantity = order.Quantity,
                Price = order.Price,
                TotalAmount = order.Quantity * order.Price
            };
        }

        public async Task<OrdersDto> AddOrderAsync(AddOrdersDto dto, int userId)
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

            return new OrdersDto
            {
                Id = order.Id,
                ProductName = order.ProductName,
                Quantity = order.Quantity,
                Price = order.Price,
                TotalAmount = order.Quantity * order.Price
            };
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

        public async Task PatchOrderAsync(int orderId, int userId, PatchOrderDto dto)
        {
            var order = await _repo.GetOrderByOrderId(orderId);

            if (order == null)
                throw new Exception("Order not found");

            // Authorization check (important)
            if (order.UserId != userId)
                throw new Exception("Unauthorized");

            if (dto.ProductName != null)
                order.SetProductName(dto.ProductName);

            if (dto.Quantity.HasValue)
                order.SetQuantity(dto.Quantity.Value);

            if (dto.Price.HasValue)
                order.SetPrice(dto.Price.Value);


            await _repo.UpdateOrdersAsync();

            // Clear cache (since you already use cache)
            _cache.Remove($"orders_user_{order.UserId}");
        }
        public async Task DeleteOrdersAsync(int orderId, int userId)
        {
            await _repo.DeleteOrdersAsync(orderId);
            _cache.Remove($"orders_user_{userId}");
        }
    }
}
