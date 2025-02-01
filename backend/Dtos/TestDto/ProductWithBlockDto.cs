namespace backend.Dtos.TestDto;

public class ProductWithBlockDto
{
    public String ProductName { get; set; }
    public List<BlockDto>? ProductBlocks { get; set; } = new List<BlockDto>();
}