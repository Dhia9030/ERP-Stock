namespace StockManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BuyOrder: Order
{
    [Display(Name = "Supplier ID")]
    public int? SupplierId { get; set; }

    [ForeignKey("SupplierId")]
    public Supplier? Supplier { get; set; }
    
}