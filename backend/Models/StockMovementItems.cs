using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace StockManagement.Models
{
    public class StockMovementItems
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int StockMovementItemId { get; set; } 
        
        [Required(ErrorMessage = "L'ID de l'élément de produit est obligatoire")]
        [ForeignKey("ProductItemId")]
        public int ProductItemId { get; set; }

        [ValidateNever] 
        [Required(ErrorMessage = "Le produitItem doit être associé")]
        [Display(Name = "ProductItem")]
        public ProductItem ProductItem { get; set; }

        // Clé étrangère vers StockMovement
        [Required(ErrorMessage = "L'ID du mouvement de stock est obligatoire")]
        [ForeignKey("StockMovementId")]
        public int StockMovementId { get; set; }

        [ValidateNever]
        [Required(ErrorMessage = "Le StockMovement doit être associé")]
        [Display(Name = "StockMovement")]
        public StockMovement StockMovement { get; set; }
    }
}