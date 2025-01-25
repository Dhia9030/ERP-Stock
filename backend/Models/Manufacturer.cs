using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagement.Models
{
    public class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManufacturerId { get; set; } // Unique identifier for the manufacturer

        [Required(ErrorMessage = "Name is required.")]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } // Name of the manufacturer

        [Column(TypeName = "varchar(255)")]
        public string Address { get; set; } // Address of the manufacturer

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; } // Contact email of the manufacturer

        [Required(ErrorMessage = "Phone number is required.")]
        [Column(TypeName = "varchar(20)")]
        public string Phone { get; set; } // Contact phone number of the manufacturer

        // Relationship: A manufacturer can produce multiple products
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}