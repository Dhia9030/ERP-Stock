using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagement.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; } // Unique identifier for the supplier
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; } // Supplier's registration date

        // Relationship: A supplier can supply multiple products
        [NotMapped]
        public ICollection<Product> Products { get; set; }
        
        public ICollection<BuyOrder> Orders { get; set; }
    }
}
