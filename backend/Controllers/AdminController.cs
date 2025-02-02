using backend.Models;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IStockManagerService _stockManagerService;
    
    public AdminController(IStockManagerService stockManagerService)
    {
        _stockManagerService = stockManagerService;
    }
    
    [HttpGet("getstockmanagers")]
    public async Task<IActionResult> GetStockManagers()
    {
        var stockManagers = await _stockManagerService.GetStockManagersList();
        return Ok(stockManagers.Select(sm => new
        {
            sm.UserName,
            sm.Email
        }));
    }
    
    
    
    [HttpPost("addnewstockmanager")]
    public async Task<IActionResult> AddNewStockManager(RegisterCredentials registerCredentials)
    {
        try
        {
            var result = await _stockManagerService.AddNewStockManager(registerCredentials);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
            
        }
        
    }
    
    [HttpDelete("deletestockmanager")]
    
    public async Task<IActionResult> DeleteStockManager(string email)
    {
        try
        {
            var result = await _stockManagerService.DeleteStockManager(email);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}