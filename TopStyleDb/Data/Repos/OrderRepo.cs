using Microsoft.EntityFrameworkCore;
using TopStyleDb.Data.Interfaces;
using TopStyleDb.Models.DTO;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Data.Repos
{
    public class OrderRepo : IOrderRepo
    {
        private readonly TopStyleDbContext _context;
        public OrderRepo(TopStyleDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetCurrentUserOrders(int userId)
        {
            return await _context.Orders.Where(o => o.CustomerId == userId).ToListAsync();
        }

        public async Task<Order> GetOrderById(int id, int userId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id && o.CustomerId == userId);
        }
    }
}
