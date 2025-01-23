using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockManagement.Models
{
    public class Order
    {
        public int OrderId { get; set; } // Unique identifier for the order
        public DateTime OrderDate { get; set; } // Date of the order
        public decimal TotalAmount { get; set; } // Total amount of the order
        [Range(0, 100, ErrorMessage = "The discount percentage must be between 0 and 100.")]
        public double DiscountPercentage { get; set; }

        // Relationship: An order belongs to a client
        public int ClientId { get; set; }
        public Client Client { get; set; }

        // Relationship: An order can contain multiple products
        public ICollection<OrderProduct> OrderProducts { get; set; }
        
    }

    // Linking class between Order and Product (many-to-many relationship)
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
