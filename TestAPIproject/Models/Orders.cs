namespace TestAPIproject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class OrdersDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateOrders
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public int Quantity { get; set; }
        public decimal price { get; set; }
    }
}
