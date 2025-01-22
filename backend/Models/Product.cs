using System.ComponentModel.DataAnnotations;

namespace StockManagement.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Le nom du produit est requis.")]
        [MaxLength(150, ErrorMessage = "Le nom du produit ne peut pas dépasser 150 caractères.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La quantité en stock doit être au moins 0.")]
        public int StockQuantity { get; set; }

        [Range(0, 100, ErrorMessage = "Le pourcentage de réduction doit être entre 0 et 100.")]
        public double DiscountPercentage { get; set; }

        // Relation avec Category
        [Required(ErrorMessage = "Une catégorie est requise.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
