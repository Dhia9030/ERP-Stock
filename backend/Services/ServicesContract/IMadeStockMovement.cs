namespace backend.Services.ServicesContract;

public interface IMadeStockMovement
{
    public Task<bool> TransferProductBlockAsync(int productBlockId, int newLocationId);
    public Task<bool> MergeProductBlocksAsync(int sourceBlockId, int destinationBlockId);
    public Task<bool> DeleteProductBlockAsync(int productBlockId);
}