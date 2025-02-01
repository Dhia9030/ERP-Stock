using StockManagement.Models;

namespace backend.Dtos.TestDto;

public class StockMovementDto
{
    public int StockMovementId { get; set; }
    public string Createdby { get; set; }
    public int Quantity { get; set; }
    public DateTime MovementDate { get; set; }
    public StockMovementStatus MovementType { get; set; }
    
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    
    public int SourceBlockId { get; set; }
    public string SourceLocationName { get; set; }
    public int DestinationBlockId { get; set; }
    public string DestinationLocationName { get; set; }


    public List<int> productItemIds { get; set; }

}