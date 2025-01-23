using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagement.Models
{
    public abstract class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Le nom du produit est requis.")]
        [MaxLength(150, ErrorMessage = "Le nom du produit ne peut pas dépasser 150 caractères.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La quantité en stock doit être au moins 0.")]
        public int StockQuantity { get; set; }

        // Relation avec Category
        [Required(ErrorMessage = "Une catégorie est requise.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        // Foreign key to Manufacturer
        [Required(ErrorMessage = "Le fabricant est requis.")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        
        public ICollection<ProductItem> ProductItems { get; set; }

    }
}
