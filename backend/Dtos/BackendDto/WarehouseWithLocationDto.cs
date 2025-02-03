namespace backend.Dtos.TestDto;

public class WarehouseWithLocationDto
{
    public int warehouseId { get; set; }
    public string warehouseName { get; set; }
    public List<LocationWithBlockDto> locations { get; set; }
}