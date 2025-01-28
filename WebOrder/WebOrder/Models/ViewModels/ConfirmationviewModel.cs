using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace WebOrder.Models.ViewModels;

public class ConfirmationviewModel
{
   public int Id { get; set; } 
   
   [Microsoft.Build.Framework.Required]
   [Display(Name = "Client")]
    public int ClientId { get; set; }
    
    [Required(ErrorMessage = "The discount percentage is required.")]
    [Range(0, 100, ErrorMessage = "The discount percentage must be between 0 and 100.")]
    [Display(Name = "Discount Percentage")]
    
    public double Discount { get; set; }
    
    [Microsoft.Build.Framework.Required]
    public DateTime ExecutionDate { get; set; }
}