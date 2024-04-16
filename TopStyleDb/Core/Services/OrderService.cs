using TopStyleDb.Core.Interfaces;
using TopStyleDb.Data.Interfaces;
using TopStyleDb.Models.DTO;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _repo;
        private readonly IProductRepo _productRepo;
        public OrderService(IOrderRepo repo, IProductRepo productRepo)
        {
            _repo = repo;
            _productRepo = productRepo;
        }

        public async Task<OrderResponseDTO> CreateOrder(CreateOrderDTO createOrderDto, int userId)
        {
            try
            {
                var order = new Order
                {
                    CustomerId = userId,
                    OrderDate = DateTime.Now,
                    OrderDetails = new List<OrderDetail>()
                };

                foreach (var item in createOrderDto.OrderDetails)
                {
                    var product = await _productRepo.GetProductById(item.ProductId);
                    if (product == null)
                    {
                        throw new Exception("Product not available.");
                    }

                    order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = product.Price
                    });
                }

                var createdOrder = await _repo.CreateOrder(order);
                if (createdOrder == null)
                {
                    return null;
                }

                return new OrderResponseDTO
                {
                    OrderId = createdOrder.OrderId,
                    CustomerId = createdOrder.CustomerId,
                    OrderDate = createdOrder.OrderDate,
                    OrderDetails = createdOrder.OrderDetails.Select(od => new OrderDetailDTO
                    {
                        ProductId = od.ProductId,
                        Quantity = od.Quantity,
                        Price = od.Price,
                        Total = od.Quantity * od.Price
                    }).ToList(),
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<OrderResponseDTO>> GetCurrentUserOrders(int userId)
        {
            var orders = await _repo.GetCurrentUserOrders(userId);
            return orders.Select(o => new OrderResponseDTO
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailDTO
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price,
                    Total = od.Quantity * od.Price
                }).ToList()
            }).ToList();
        }

        public async Task<OrderResponseDTO> GetOrderById(int id, int userId)
        {
            var order = await _repo.GetOrderById(id, userId);
            if (order == null)
            {
                return null;
            }

            return new OrderResponseDTO
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price,
                    Total = od.Quantity * od.Price
                }).ToList()
            };
        }
    }
}
