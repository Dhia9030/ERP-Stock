using StockManagement.Models;

namespace backend.Dtos.TestDto;

public class BlockWithProductNameDto
{
    public int ProductBlockId { get; set; }
    public int productId { get; set; }
    public string productName { get; set; }
    public Double? DiscountPercentage { get; set; }
    public int quantity { get; set; }
    public ProductBlockStatus? Status { get; set; }
    public DateTime? ExpirationDate { get; set; } = null;
    public Dictionary<int,ProductItemStatus>? ProductItemIds { get; set; } = new Dictionary<int,ProductItemStatus>();
}