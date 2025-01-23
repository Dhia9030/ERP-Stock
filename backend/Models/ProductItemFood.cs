namespace StockManagement.Models;
using System.ComponentModel.DataAnnotations;

public class ProductItemFood : ProductItem
{
    [Required(ErrorMessage = "La date d'expiration est requise.")]
    public DateTime ExpirationDate { get; set; }
}