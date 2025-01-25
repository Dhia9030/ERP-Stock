namespace StockManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


public class ProductSupplier
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int ProductSupplierId { get; set; } 
        
    [Required(ErrorMessage = "L'ID de produit est obligatoire")]
    [ForeignKey("ProductItemId")]
    public int ProductId { get; set; }

    [ValidateNever] 
    [Required(ErrorMessage = "Le produitItem doit être associé")]
    [Display(Name = "ProductItem")]
    public Product Product { get; set; }

    // Clé étrangère vers StockMovement
    [Required(ErrorMessage = "L'ID du mouvement de stock est obligatoire")]
    [ForeignKey("StockMovementId")]
    public int SupplierId { get; set; }

    [ValidateNever]
    [Required(ErrorMessage = "Le StockMovement doit être associé")]
    [Display(Name = "StockMovement")]
    public Supplier Supplier { get; set; }
}