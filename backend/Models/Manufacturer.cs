namespace StockManagement.Models;

public class Manufacturer
{
    public int ManufacturerId { get; set; } // Unique identifier for the manufacturer
    public string Name { get; set; } // Name of the manufacturer
    public string Address { get; set; } // Address of the manufacturer
    public string Email { get; set; } // Contact email of the manufacturer
    public string Phone { get; set; } // Contact phone number of the manufacturer

    // Relationship: A manufacturer can produce multiple products
    public ICollection<Product> Products { get; set; }
}