namespace WebOrder.Models;

public class OrderItemViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    
    public decimal ProductPrice { get; set; }
    
    public decimal Total { get; set; }
}