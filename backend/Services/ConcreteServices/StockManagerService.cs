using backend.Models;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.ConcreteServices;

public class StockManagerService : ServicesContract.IStockManagerService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public StockManagerService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<IEnumerable<IdentityUser>> GetStockManagersList()
    {
        var users = await _userManager.GetUsersInRoleAsync("StockManager");
        return users;
    }

    public async Task<IdentityResult> AddNewStockManager(RegisterCredentials registerCredentials)
    {
        var user = new IdentityUser
        {
            UserName = registerCredentials.FirstName+registerCredentials.LastName,
            Email = registerCredentials.Email
        };
        var result = await _userManager.CreateAsync(user, registerCredentials.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "StockManager");
        }
        return result;
        
    }
    
    public async Task<IdentityResult> DeleteStockManager(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if(user == null)
        {
            throw new Exception("User not found");
        }
        
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            await _userManager.RemoveFromRoleAsync(user, "StockManager");
            return result;
        }

      return IdentityResult.Failed(new IdentityError { Description = result.Errors.FirstOrDefault()?.Description ?? "Unknown error" });
        
    }
    
}