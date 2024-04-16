namespace TopStyleDb.Models.DTO
{
    public class OrderDTO
    {
    }

    public class CreateOrderDTO
    {
        public List<CreateOrderDetailsDTO> OrderDetails { get; set; }
    }

    public class CreateOrderDetailsDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; } /*=> Quantity * Price;*/
    }

    public class OrderResponseDTO
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
        public decimal Total => OrderDetails.Sum(s => s.Total);
    }
}
