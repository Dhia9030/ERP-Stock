using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum ProductItemStatus
{
        InStock,
        Sold,
        Returned
}

namespace StockManagement.Models
{
    public class ProductItem
    {
        [Key] 
        public int StockItemId { get; set; }

        [Required(ErrorMessage = "The Product ID is required.")] 
        public int ProductId { get; set; }

        [ForeignKey("ProductId")] 
        public Product Product { get; set; }

        [Required(ErrorMessage = "The Location ID is required.")] 
        public int LocationId { get; set; }

        [ForeignKey("LocationId")] 
        public Location Location { get; set; }

        [Display(Name = "Client")] 
        public int? ClientId { get; set; }

        [ForeignKey("ClientId")] 
        public Client? Client { get; set; }

        [Display(Name = "Supplier")] 
        public int? SupplierId { get; set; }

        [ForeignKey("SupplierId")] 
        public Supplier? Supplier { get; set; }

        [Display(Name = "Entry Date")]
        [NotMapped]
        [DataType(DataType.Date)] 
        public DateTime? EntryDate { get; set; } 

        [Display(Name = "Exit Date")]
        [NotMapped]
        [DataType(DataType.Date)] 
        public DateTime? ExitDate { get; set; } 

        [Required(ErrorMessage = "The status is required.")] 
        [Display(Name = "Status")] 
        public ProductItemStatus Status { get; set; }
        
        public int? PurchaseOrderId { get; set; }

        [ForeignKey("PurchaseOrderId")] 

        [Display(Name = "Purchase Order")] 
        public Order? PurchaseOrder { get; set; }
        
        public int? SaleOrderId { get; set; }

        [ForeignKey("SaleOrderId")] 

        [Display(Name = "Sale Order")] 
        public Order? SaleOrder { get; set; }

        [Range(0, 100, ErrorMessage = "The discount percentage must be between 0 and 100.")] 
        [Display(Name = "Discount Percentage")] 
        public double DiscountPercentage { get; set; }
    }
}