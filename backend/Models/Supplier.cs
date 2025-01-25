using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagement.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; } // Unique identifier for the supplier

        [Required(ErrorMessage = "Name is required.")]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Registration date is required.")]
        public DateTime RegistrationDate { get; set; } // Supplier's registration date

        [NotMapped]
        // Relationship: A supplier can supply multiple products
        public ICollection<Product> Products { get; set; } = new List<Product>();

        // Relationship: A supplier can have multiple buy orders
        public ICollection<BuyOrder> Orders { get; set; } = new List<BuyOrder>();
    }
}