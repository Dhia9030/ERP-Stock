using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebOrder.Models.ViewModels;

public class NewClientViewModel
{
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
    
    [Required]
    [Display(Name = "Discount Percentage")]
    public double Discount { get; set; }
    
    [Microsoft.Build.Framework.Required]
    public DateTime ExecutionDate { get; set; }
}