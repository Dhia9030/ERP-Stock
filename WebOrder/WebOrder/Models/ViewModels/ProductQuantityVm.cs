using System.ComponentModel.DataAnnotations;

namespace WebOrder.Models.ViewModels;

public class ProductQuantityVm
{
    public int productId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int quantity { get; set; }
}