using Microsoft.EntityFrameworkCore;
using TestAPIproject.Application.Service;
using TestAPIproject.Domain;
using TestAPIproject.Infrastructure.Data;

namespace TestAPIproject.Infrastructure.Repository
{
    public class OrdersRepository : IOrdersRepository
    {

        private readonly AppDbContext _context;
        private readonly ILogger<OrdersRepository> _logger;

        public OrdersRepository(AppDbContext context,ILogger<OrdersRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Order>> GetOrderByUserId(int userid)
        {
            //return await _context.Orders.FindAsync(userid) ;
            return await _context.Orders.Where(o => o.UserId == userid).AsNoTracking().ToListAsync();
        }

        public async Task<Order?> GetOrderByOrderId(int orderId)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);

                await _context.SaveChangesAsync();
            _logger.LogWarning("Order with Id {OrderId} is added successfully", order.Id);

        }

        public async Task UpdateOrdersAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task DeleteOrdersAsync(int id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);

                if (order == null)
                {
                    _logger.LogWarning("Order with Id {OrderId} not found for deletion", id);
                    return;
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting order with Id {OrderId}", id);
                throw;
            }
        }
    }
}
