using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public class ProductController: Controller
{
    private readonly IGetProductService _getProductService;
    private readonly IProductWithBlocksService _productWithBlocksService;
    
    public ProductController(IGetProductService getProductService, IProductWithBlocksService productWithBlocksService)
    {
        _getProductService = getProductService;
        _productWithBlocksService = productWithBlocksService;
    }
    
    
    
    [Route("GetAllProduct")]
    [HttpGet]
    public async Task<IActionResult> Index5()
    {
        var products = await _getProductService.GetAllProductsAsync();
        
        foreach (var product in products)
        {
            if (product.Category != null)
            {
                product.Category.Products = null;
            }
        }
        
        return Json(products ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("GetAllClothingProduct")]
    [HttpGet]
    public async Task<IActionResult> Index6()
    {
        var products = await _getProductService.GetAllClothingProduct();
        foreach (var product in products)
        {
            if (product.Category != null)
            {
                product.Category.Products = null;
            }
        }
        return Json(products ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
          
            
        });
    }
    
    
    
    [Route("GetAllElectronicProduct")]
    [HttpGet]
    public async Task<IActionResult> Index19()
    {
        var products = await _getProductService.GetAllElectronicProduct();
        
        foreach (var product in products)
        {
            if (product.Category != null)
            {
                product.Category.Products = null;
            }
        }
        
        return Json(products ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
        });
    }
    
    
    
    [Route("GetAllFoodProduct")]
    [HttpGet]
    public async Task<IActionResult> Index20()
    {
        var products = await _getProductService.GetAllFoodProduct();
        
        foreach (var product in products)
        {
            if (product.Category != null)
            {
                product.Category.Products = null;
            }
        }
        return Json(products ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
          
            
        });
    }
    
    
    
    [Route("get all product with blocks")]
    [HttpGet]
    public async Task<IActionResult> Index11()
    {
        var products = await _productWithBlocksService.GetAllProductWithBlocks();
        return Json(products ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    
    
}