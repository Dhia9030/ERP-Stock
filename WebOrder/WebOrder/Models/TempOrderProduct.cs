using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.Operations;

namespace WebOrder.Models;

public class TempOrderProduct
{
    
    
    public int TempOrderId { get; set; }

 
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("TempOrderId")]
    public virtual TempOrder TempOrderOrder { get; set; }
    
    
    
}