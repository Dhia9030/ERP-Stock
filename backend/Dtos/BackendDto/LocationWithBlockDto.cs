namespace backend.Dtos.TestDto;

public class LocationWithBlockDto
{
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    
    public bool isEmpty { get; set; }
    public BlockWithProductNameDto? Block { get; set; }
}