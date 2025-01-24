namespace StockManagement.Models;
public enum StockMovementStatus
{
    Incoming,   
    Outgoing, 
    Transfer
}

public class StockMovement
{
    public int StockMovementId { get; set; }

    public string SourceLocation { get; set; }

    public string DestinationLocation { get; set; }

    public StockMovementStatus MovementType { get; set; }

    // Optional: Reference to the user who initiated the movement
    public string CreatedBy { get; set; }

    public int? OrderId { get; set; }
    public Order Order { get; set; }
    
    public DateTime MovementDate { get; set; } 
    
    public ICollection<StockMovementPerItem>? StockMovementPerItem { get; set; } 
    
}