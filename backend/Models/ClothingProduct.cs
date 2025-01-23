using System.ComponentModel.DataAnnotations;

namespace StockManagement.Models
{
    public class ClothingProduct : Product
    {
        [Required(ErrorMessage = "Le type de tissu est requis.")]
        [MaxLength(50)]
        public string FabricType { get; set; }

        [Required(ErrorMessage = "La taille est requise.")]
        [MaxLength(5)]
        public string Size { get; set; } // Ex: "S", "M", "L", etc.
    }
}
