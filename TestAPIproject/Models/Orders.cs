//using FluentValidation;

namespace TestAPIproject.Models
{
    /*  public class Order
      {
          public int Id { get; set; }
          public int UserId { get; set; }
          public string ProductName { get; set; }
          public int Quantity { get; set; }
          public decimal Price { get; set; }
          public DateTime OrderDate { get; set; }
      } */

    public class Order
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public DateTime OrderDate { get; private set; }

        public Order(int userId, string productName, int quantity, decimal price)
        {
            SetProductName(productName);
            SetQuantity(quantity);
            SetPrice(price);

            UserId = userId;
            OrderDate = DateTime.UtcNow;
        }

        // ✅ Add these methods (IMPORTANT)
        public void SetProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new Exception("Invalid product name");

            ProductName = productName;
        }

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new Exception("Invalid quantity");

            Quantity = quantity;
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
                throw new Exception("Invalid price");

            Price = price;
        }
    }

}
