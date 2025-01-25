
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagement.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; } // Unique identifier for the client

        [Required(ErrorMessage = "Last name is required.")]
        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Column(TypeName = "varchar(20)")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Registration date is required.")]
        public DateTime RegistrationDate { get; set; } // Client's registration date

        // Relationship: A client can have multiple orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}