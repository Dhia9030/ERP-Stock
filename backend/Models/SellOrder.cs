using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagement.Models;

public class SellOrder : Order
{
    [NotMapped]
    public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
    
}