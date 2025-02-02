using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.ServicesContract;

public interface IStockManagerService
{
    public Task<IEnumerable<IdentityUser>> GetStockManagersList();
    public Task<IdentityResult> AddNewStockManager( RegisterCredentials registerCredentials);
    public Task<IdentityResult> DeleteStockManager(string email);
}