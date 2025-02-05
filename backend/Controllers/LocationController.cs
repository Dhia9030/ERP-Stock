using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin, StockManager")]
public class LocationController : Controller
{
    private readonly ILocationService _locationService;
    
    public LocationController(ILocationService LocationService)
    {
        _locationService = LocationService;
    }
    
    
        
    [Route("GetAllLocation")]
    [HttpGet]
    public async Task<IActionResult> Index8()
    {
        var locations = await _locationService.GetAllLocations();
        
        foreach(var location in locations)
        {
            if(location.Warehouse != null)
            {
                location.Warehouse.Locations = null;
            }
        }
        
        return Json(locations ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
            
            
        });
    }
    
    
    [Route("GetFreeLocation")]
    [HttpGet]
    public async Task<IActionResult> Index9()
    {
        var locations = await _locationService.GetFreeLocations();
        
        foreach(var location in locations)
        {
            if(location.Warehouse != null)
            {
                location.Warehouse.Locations = null;
            }
        }
        
        return Json(locations ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    
}