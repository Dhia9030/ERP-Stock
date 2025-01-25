using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StockManagement.Models;

public class StockMovementPerItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StockMovementPerItemId { get; set; }

    [Required(ErrorMessage = "Le produit est obligatoire")]
    [ForeignKey("Product")]
    [Display(Name = "Produit")]
    public int ProductId { get; set; }

    [ValidateNever]
    [Required(ErrorMessage = "Le produit doit être associé")]
    [Display(Name = "Produit")]
    public Product Product { get; set; }

    [Required(ErrorMessage = "La quantité est obligatoire")]
    [Range(0, int.MaxValue, ErrorMessage = "La quantité ne peut pas être négative")]
    [Display(Name = "Quantité")]
    public int Quantity { get; set; }

    [ValidateNever]
    [Display(Name = "Items de produit")]
    public ICollection<ProductItem>? ProductItems { get; set; }
}