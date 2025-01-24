namespace WebOrder.Models.ViewModels;

public class OrderViewModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public List<OrderItemViewModel> Items { get; set; }
}