namespace StockManagement.Models;

public class StockMovementPerItem
{
    public int StockMovementPerItemId { get; set; }
    
    public int ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public int Quantity { get; set; }  
    
    public ICollection<ProductItem>? ProductItems { get; set; } 
}