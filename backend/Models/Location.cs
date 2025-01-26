using System.ComponentModel.DataAnnotations;

namespace StockManagement.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Le nom de l'emplacement est requis.")]
        [MaxLength(100, ErrorMessage = "Le nom de l'emplacement ne peut pas dépasser 100 caractères.")]
        public string Name { get; set; } 

        [Required(ErrorMessage = "L'entrepôt est requis.")]
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

    }
}
