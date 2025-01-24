using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
        public int? StockMovementId {get; set;}
        public StockMovement StockMovement {get; set;}
        
        [Column(TypeName = "decimal(18, 3)")]
        public decimal TotalAmount { get; set; } // Total amount of the order

        [Range(0, 100, ErrorMessage = "The discount percentage must be between 0 and 100.")]
        public double DiscountPercentage { get; set; }

        public OrderStatus Status { get; set; } 

        public OrderType Type { get; set; } 

        // Foreign key for Supplier (if the order is a purchase order)
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        // Foreign key for Client (if the order is a sales order)
        public int? ClientId { get; set; }
        public Client? Client { get; set; }

        public ICollection<StockMovementPerItem>? StockMovementPerItem { get; set; }
    }
}