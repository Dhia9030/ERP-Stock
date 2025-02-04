using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PredictionController : Controller
{
    private readonly IPredectionService _predectionService;
    
    public PredictionController(IPredectionService predectionService)
    {
        _predectionService = predectionService;
    }
    
    [HttpGet]
    
    
    public async Task<IActionResult> GetPredection()
    {
        var predections = await _predectionService.GetPredection();
        return Json(predections);
    }
    
    [HttpGet("PredictionForAllCategories")]
    public async Task<IActionResult> GetPredectionForAllCategories()
    {
        var predections = await _predectionService.GetPredectionForAllCategories();
        return Json(predections);
    }
}