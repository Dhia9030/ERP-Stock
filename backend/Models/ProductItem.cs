using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum ProductItemStatus
{
        InStock,
        Sold,
        Returned,
        Expired
}

namespace StockManagement.Models
{
    public class ProductItem
    {
        [Key] 
        public int ProductItemId { get; set; }

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
        
        public int? ProductBlockId { get; set; }
        [ForeignKey("ProductBlockId")] 
        [Display(Name = "Product Block")] 
        public ProductBlock? ProductBlock { get; set; }
        
      
    }
}