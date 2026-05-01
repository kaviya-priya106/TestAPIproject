namespace TestAPIproject.Dto
{
    public class OrdersDto
    {
    }

    public class AddOrdersDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateOrdersDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    /*public class UpdateOrders
   {
       public int Id { get; set; }
       public string ProductName { get; set; } ✅
       public int Quantity { get; set; }
       public decimal Price { get; set; } ✅
   }*/
}
