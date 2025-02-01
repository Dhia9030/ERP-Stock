using StockManagement.Models;

namespace backend.Dtos.TestDto;

public class BlockDto
{
    public int ProductBlockId { get; set; }
    public string LocationName{ get; set; }
    public Double DiscountPercentage { get; set; }
    public int quantity { get; set; }
    public ProductBlockStatus? Status { get; set; }
    public DateTime? ExpirationDate { get; set; } = null;
    public Dictionary<int,ProductItemStatus>? ProductItemIds { get; set; } = new Dictionary<int,ProductItemStatus>();
}