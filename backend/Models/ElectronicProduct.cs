using System.ComponentModel.DataAnnotations;

namespace StockManagement.Models
{
    public class ElectronicProduct : Product
    {
        [Required(ErrorMessage = "La durée de garantie est requise.")]
        [Range(0, 10, ErrorMessage = "La garantie doit être comprise entre 0 et 10 ans.")]
        public int WarrantyYears { get; set; }

        [Required(ErrorMessage = "La classe énergétique est requise.")]
        [MaxLength(5)]
        public string EnergyClass { get; set; }
    }
}
