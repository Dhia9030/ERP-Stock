using System.ComponentModel.DataAnnotations;

public enum OrderStatus
{
    Pending,    // Order is created but not yet processed
    Processing, // Order is being processed
    Delivered,  // Order has been delivered
    Cancelled   // Order has been cancelled
}

public enum OrderType
{
    Purchase, // Order from a supplier
    Sales     // Order to a client
}

namespace StockManagement.Models
{
    public class Order
    {
        public int OrderId { get; set; } // Unique identifier for the order
        public DateTime OrderDate { get; set; } // Date of the order
        public decimal TotalAmount { get; set; } // Total amount of the order

        [Range(0, 100, ErrorMessage = "The discount percentage must be between 0 and 100.")]
        public double DiscountPercentage { get; set; }

        public OrderStatus Status { get; set; } // Status of the order

        public OrderType Type { get; set; } // Type of the order (Purchase or Sales)

        // Foreign key for Supplier (if the order is a purchase order)
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        // Foreign key for Client (if the order is a sales order)
        public int? ClientId { get; set; }
        public Client? Client { get; set; }

        // Relationship: An order can contain multiple products
        public ICollection<OrderProduct>? OrderProducts { get; set; } // each item represents a different product in the order

        // Relationship: An order can contain multiple product items
        public ICollection<ProductItem>? ProductItems { get; set; } // it has all the product items in the order
    }
}