using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin, StockManager")]
public class GetOrderController : Controller
{
    private readonly IGetOrderService _getOrderService;
    
    public GetOrderController(IGetOrderService getOrderService)
    {
        _getOrderService = getOrderService;
    }
    
    
    
    [Route("GetAllSellsOrder")]
    [HttpGet]
    public async Task<IActionResult> Index3()
    {
        var order = await _getOrderService.GetAllSellOrders();
        order = order.Reverse().ToList();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
        });
    }
    
    
    
    [Route("GetAllBuyOrder")]
    [HttpGet]
    public async Task<IActionResult> Index7()
    {
        var order = await _getOrderService.GetAllBuyOrders();
        order = order.Reverse().ToList();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
        });
    }
    
    
    [Route("DetailOfAnOrder")]
    [HttpGet]
    public async Task<IActionResult> Index4(int id)
    {
        var order = await _getOrderService.GetOrderDetail(id);
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
        });
    }
    
    
    
    
}