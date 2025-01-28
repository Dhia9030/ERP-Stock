namespace WebOrder.Models.ViewModels;

public class CommandeViewModel
{
    public Client Client { get; set; }
    public Order Order { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }

}