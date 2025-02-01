using StockManagement.Models;

namespace backend.Dtos.TestDto;

public class OrderDto
{
    public Order Order { get; set; }
    public List<ProductWithBlockDto> ProductWithBlocks { get; set; }
}