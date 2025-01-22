using System;

namespace StockManagement.Models
{
    public class FoodProduct : Product
    {
        [Required(ErrorMessage = "La date d'expiration est requise.")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "La température de stockage est requise.")]
        [Range(-30, 30, ErrorMessage = "La température doit être comprise entre -30°C et 30°C.")]
        public int StorageTemperature { get; set; }
    }
}
