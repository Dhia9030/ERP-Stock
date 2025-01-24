namespace WebOrder.Models;

public class TempOrder
{
    public int TempOrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int? ClientId { get; set; }

   
    public virtual ICollection<TempOrderProduct> TempOrderProducts { get; set; } = new List<TempOrderProduct>();

}
