using System.ComponentModel.DataAnnotations;

namespace StockManagement.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Le nom de la catégorie est requis.")]
        [MaxLength(100, ErrorMessage = "Le nom de la catégorie ne peut pas dépasser 100 caractères.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        public string? Description { get; set; }

        // Relation avec les produits
        public List<Product> Products { get; set; } = new List<Product>();
    }
}