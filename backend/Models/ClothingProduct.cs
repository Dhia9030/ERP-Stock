using System.ComponentModel.DataAnnotations;

public enum ClothingProductStatus
{
    S,
    M,
    L
}


namespace StockManagement.Models
{
    public class ClothingProduct : Product
    {
        [Required(ErrorMessage = "Le type de tissu est requis.")]
        [MaxLength(50)]
        public string FabricType { get; set; }

        [Required(ErrorMessage = "La taille est requise.")]
        [MaxLength(5)]
        public ClothingProductStatus Size { get; set; } 
    }
}
