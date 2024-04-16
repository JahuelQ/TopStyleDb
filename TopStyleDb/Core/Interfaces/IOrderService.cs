using TopStyleDb.Models.DTO;

namespace TopStyleDb.Core.Interfaces
{
    public interface IOrderService
    {
        public Task<List<OrderResponseDTO>> GetCurrentUserOrders(int userId);
        public Task<OrderResponseDTO> GetOrderById(int id, int userId);
        public Task<OrderResponseDTO> CreateOrder(CreateOrderDTO createOrderDto, int userId);
    }
}
