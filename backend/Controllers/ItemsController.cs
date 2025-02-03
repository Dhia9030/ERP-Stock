using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Dtos.TestDto;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public class ItemsController : Controller
{
    private readonly IStockMovementService _stockMovementService;
    private readonly IOrderProductServices _orderProductServices;
    
    public ItemsController(IStockMovementService stockMovementService, IOrderProductServices orderProductServices)
    {
        _stockMovementService = stockMovementService;
        _orderProductServices = orderProductServices;
    }

    
    
    
    [Route("GetItemFromASpecificBuyOrderAndAProduct")]
    [HttpGet]
    public async Task<IActionResult> Index14(int OrderId , int ProductId)
    {
        var productWithItems = await _stockMovementService.GetItemsForEachProductForSpecificBuyOrder(OrderId, ProductId);
        return Json(productWithItems ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    
    [Route("GetItemFromASpecificOrderForAllProduct")]
    [HttpGet]
    
    public async Task<IActionResult> Index15(int OrderId)
    {
        var productsWithItems = await _orderProductServices.getProductForSpecificOrder(OrderId);
        List<ProductWithItemsDto> productWithItemsDtos = new List<ProductWithItemsDto>();
        
        foreach (var product in productsWithItems)
        {
            var items = await _stockMovementService.GetItemsForEachProductForSpecificBuyOrder(OrderId, product.ProductId);
            productWithItemsDtos.Add(items);
        }
        return Json( productWithItemsDtos ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("GetItemFromASpecificSellOrderAndAProduct")]
    [HttpGet]
    public async Task<IActionResult> Index16(int OrderId , int ProductId)
    {
        var productWithItems = await _stockMovementService.GetItemsForEachProductForSpecificSellOrder(OrderId, ProductId);
        return Json(productWithItems ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("GetItemFromASpecificSellOrderForAllProduct")]
    [HttpGet]
    
    public async Task<IActionResult> Index17(int OrderId)
    {
        var productlist =  await _orderProductServices.getProductForSpecificOrder(OrderId);
        List<ProductWithItemsDto> productWithItemsDtos = new List<ProductWithItemsDto>();
        
        foreach (var product in productlist)
        {
            var items = await _stockMovementService.GetItemsForEachProductForSpecificSellOrder(OrderId, product.ProductId);
            productWithItemsDtos.Add(items);
        }
        return Json( productWithItemsDtos ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    
    
}