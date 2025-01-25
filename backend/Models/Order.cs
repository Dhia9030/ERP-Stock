using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum OrderStatus
{
    Pending,
    Processing,
    Delivered,
    Cancelled
}

public enum OrderType
{
    Purchase,
    Sales
}

namespace StockManagement.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Display(Name = "Stock Movement ID")]
        public int? StockMovementId { get; set; }

        [ForeignKey("StockMovementId")]
        public StockMovement? StockMovement { get; set; }

        [Required(ErrorMessage = "The total amount is required.")]
        [Column(TypeName = "decimal(18, 3)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The total amount must be greater than 0.")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "The discount percentage is required.")]
        [Range(0, 100, ErrorMessage = "The discount percentage must be between 0 and 100.")]
        [Display(Name = "Discount Percentage")]
        public double DiscountPercentage { get; set; }

        [Required(ErrorMessage = "The order status is required.")]
        [Display(Name = "Order Status")]
        public OrderStatus Status { get; set; }

        [Required(ErrorMessage = "The order type is required.")]
        [Display(Name = "Order Type")]
        public OrderType Type { get; set; }

        [Display(Name = "Supplier ID")]
        public int? SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }

        [Display(Name = "Client ID")]
        public int? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client? Client { get; set; }

        [Display(Name = "Stock Movement Per Item")]
        public ICollection<StockMovementPerItem>? StockMovementPerItems { get; set; }
    }
}