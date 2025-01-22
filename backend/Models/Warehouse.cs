using System.ComponentModel.DataAnnotations;

namespace StockManagement.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }

        [Required(ErrorMessage = "Le nom de l'entrepôt est requis.")]
        [MaxLength(150, ErrorMessage = "Le nom de l'entrepôt ne peut pas dépasser 150 caractères.")]
        public string Name { get; set; }

        [MaxLength(300, ErrorMessage = "L'adresse ne peut pas dépasser 300 caractères.")]
        public string? Location { get; set; }

        // Relation avec les stocks
        public List<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    }
}
