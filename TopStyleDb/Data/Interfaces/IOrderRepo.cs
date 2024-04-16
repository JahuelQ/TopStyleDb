using TopStyleDb.Models.DTO;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Data.Interfaces
{
    public interface IOrderRepo
    {
        public Task<List<Order>> GetCurrentUserOrders(int userId);
        public Task<Order> GetOrderById(int id, int userId);
        public Task<Order> CreateOrder(Order order);

    }
}
