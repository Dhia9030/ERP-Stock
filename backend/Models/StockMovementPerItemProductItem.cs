
namespace StockManagement.Models
{

    public class StockMovementPerItemProductItem
    {
        public int ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }
        public int StockMovementPerItemId { get; set; }
        public StockMovementPerItem StockMovementPerItem { get; set; }
    }
}