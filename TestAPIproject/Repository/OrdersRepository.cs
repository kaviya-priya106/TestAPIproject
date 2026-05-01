using Microsoft.EntityFrameworkCore;
using TestAPIproject.Data;
using TestAPIproject.Models;

namespace TestAPIproject.Repository
{
    public class OrdersRepository:IOrdersRepository
    {

        private AppDbContext _context;

        public OrdersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order?>> GetOrderByUserId(int userid)
        {
            //return await _context.Orders.FindAsync(userid) ;
            return await _context.Orders.Where(o => o.UserId == userid).AsNoTracking().ToListAsync();
        }

        public async Task<Order?> GetOrderByOrderId(int orderd)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderd);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task UpdateOrdersAsync()
        {
            await _context.SaveChangesAsync();
        }



        public async Task DeleteOrdersAsync(int id)
        {
            var ord = await _context.Orders.FindAsync(id);
            if (ord != null)
            {
                _context.Orders.Remove(ord);
                await _context.SaveChangesAsync();
            }
        }
    }
}
