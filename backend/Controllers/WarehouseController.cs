using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;


[Route("api/[controller]")]
[ApiController]
public class WarehouseController : Controller
{
    private readonly IWarehouseService _warehouseService;
    
    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }
    
    [Route("getWarehouseWithLocations")]
    [HttpGet]
    public async Task<IActionResult> Index21(int warehouseId)
    {
        var warehouseWithLocations = await _warehouseService.getWarehouseWithLocations(warehouseId);
        return Json(warehouseWithLocations ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    
}