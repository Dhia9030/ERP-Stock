using System.ComponentModel.DataAnnotations;

namespace StockManagement.Models
{
    public class StockMovement
    {
        [Key]
        public int StockMovementId { get; set; }

        [Required(ErrorMessage = "Un produit est requis.")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "Un entrepôt est requis.")]
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        [Required(ErrorMessage = "Un emplacement est requis.")]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        [Required(ErrorMessage = "L'id de commande est requis.")]
        public int OrderId { get; set; }
        public Order Order { get; set; }


        [Required(ErrorMessage = "La quantité est requise.")]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être au moins 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Le type de mouvement est requis.")]
        [RegularExpression("IN|OUT", ErrorMessage = "Le type de mouvement doit être 'IN' ou 'OUT'.")]
        public string MovementType { get; set; } // "IN" ou "OUT"

        [Required]
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
    }
}
