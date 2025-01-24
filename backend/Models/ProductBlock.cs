namespace StockManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public enum ProductBlockStatus
{
    InStock,
    Sold,
    Expired
}

public class ProductBlock
{
    [Key]
    public int ProductBlockId { get; set; }
    
    [Required(ErrorMessage = "The Product ID is required.")] 
    public int ProductId { get; set; }
    [ForeignKey("ProductId")] 
    public Product Product { get; set; }
    
    [Required(ErrorMessage = "The Location ID is required.")] 
    public int LocationId { get; set; }
    [ForeignKey("LocationId")] 
    public Location Location { get; set; }
    
    public int? PurchaseOrderId { get; set; }
    [ForeignKey("PurchaseOrderId")] 
    [Display(Name = "Purchase Order")] 
    public Order? PurchaseOrder { get; set; }

    [Display(Name = "Exit Date")]
    [NotMapped]
    [DataType(DataType.Date)] 
    public DateTime? ExitDate { get; set; } 
    
    [Range(0, 100, ErrorMessage = "The discount percentage must be between 0 and 100.")] 
    [Display(Name = "Discount Percentage")] 
    public double DiscountPercentage { get; set; }
    
    public int Quantity { get; set; } 
    
    public ProductBlockStatus Status { get; set; }
    
    public ICollection<ProductItem> ProductItems { get; set; }
}