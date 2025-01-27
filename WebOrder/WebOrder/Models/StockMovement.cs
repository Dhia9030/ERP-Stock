using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class StockMovement
{
    public int StockMovementId { get; set; }

    public int MovementType { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime MovementDate { get; set; }

    public int SourceProductBlockId { get; set; }

    public int DestinationProductBlockId { get; set; }

    public int SourceLocationId { get; set; }

    public int DestinationLocationId { get; set; }

    public int Quantity { get; set; }

    public int? OrderId { get; set; }

    public virtual Location DestinationLocation { get; set; } = null!;

    public virtual ProductBlock DestinationProductBlock { get; set; } = null!;

    public virtual Order? Order { get; set; }

    public virtual Location SourceLocation { get; set; } = null!;

    public virtual ProductBlock SourceProductBlock { get; set; } = null!;

    public virtual ICollection<StockMovementItem> StockMovementItems { get; set; } = new List<StockMovementItem>();
}
