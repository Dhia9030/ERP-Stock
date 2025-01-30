using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum OrderStatus
{
    Pending,
    Processing,
    Delivered,
    Cancelled,
    Delayed,
    Received
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

        [Required(ErrorMessage = "The total amount is required.")]
        [Column(TypeName = "decimal(18, 3)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The total amount must be greater than 0.")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
        
        [Required(ErrorMessage = "La date du creation du commande est obligatoire")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date de creation de commande")]
        public DateTime CreationDate { get; set; }
        
        [Required(ErrorMessage = "La date d'execution de la commande est obligatoire")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date d'execution de la commande")]
        public DateTime ExecutionDate { get; set; }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "les Dates de retard de la commande")]
        public string? DelayedDates { get; set; }
        
        [Required(ErrorMessage = "La date d'execution reel de la commande est obligatoire")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date d'execution reel de la commande")]
        public DateTime RealExecutionDate { get; set; } 

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
        
        [Display(Name = "Client ID")]
        public int? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client? Client { get; set; }

        [Required(ErrorMessage = "The Warehouse ID is required.")]
        [Display(Name = "Warehouse ID")]
        public int WarehouseId { get; set; } = 1;

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
        
        [Display(Name = "Order Products")]
        public ICollection<OrderProducts>? OrderProducts { get; set; }
        
        [NotMapped]
        [Display(Name = "Item")] 
        public ICollection<ProductItem>? ProductItems { get; set; }
        
        [Display(Name = "Stock Movements of the order")]
        public ICollection<StockMovement>? StockMovements { get; set; }
        
    }
}