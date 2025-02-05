using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Dtos.TestDto;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using StockManagement.Repositories;

namespace backend.Controllers;
[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    private readonly  AppDbContext _db;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderProductsRepository _orderProductsRepository;
    private readonly IGetOrderService _getOrderService;
    private readonly IGetProductService _getProductService;
    private readonly ILocationService _locationService;
    private readonly IStockMovementService _stockMovementService;
    private readonly IProductWithBlocksService _productWithBlocksService;
    private readonly IMadeStockMovement _madeStockMovement;
    private readonly IWarehouseService _warehouseService;
    private readonly IPredectionService _predectionService;
    
    public TestController(AppDbContext db , IOrderRepository orderRepository, 
        IOrderProductsRepository orderProductsRepository , 
        IGetOrderService getOrderService, IGetProductService getProductService
        ,ILocationService locationService , IStockMovementService stockMovementService
        ,IProductWithBlocksService productWithBlocksService
        ,IMadeStockMovement madeStockMovement
        ,IWarehouseService warehouseService
        ,IPredectionService predectionService)
    {
        _db = db;
        _orderRepository = orderRepository;
        _orderProductsRepository = orderProductsRepository;
        _getOrderService = getOrderService;
        _getProductService = getProductService;
        _locationService = locationService;
        _stockMovementService = stockMovementService;
        _productWithBlocksService = productWithBlocksService;
        _madeStockMovement = madeStockMovement;
        _warehouseService = warehouseService;
        _predectionService = predectionService;
        
    }
    
    [Route("get all Order")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var buyOrders = await _orderRepository.GetAllSellOrdersAsync();
        return Json(buyOrders);
    }
    [Route("get all order with include")]
    [HttpGet]
    public async Task<IActionResult> Index2()
    {
        var order = await _orderProductsRepository.GetOrdersByProductIdAsync(8);
        var Orders = await _orderRepository.GetAllAsync(include => include.Include(e => e.OrderProducts).ThenInclude(e => e.Product));
        return Json(Orders , new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
            
        });
    }
    [Route("getall sells order")]
    [HttpGet]
    public async Task<IActionResult> Index3()
    {
        var order = await _getOrderService.GetAllSellOrders();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
            
        });
    }
    
    [Route("getall buy order")]
    [HttpGet]
    public async Task<IActionResult> Index7()
    {
        var order = await _getOrderService.GetAllBuyOrders();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
            
        });
    }
    
    [Route("detailof an order")]
    [HttpGet]
    public async Task<IActionResult> Index4(int id)
    {
        var order = await _getOrderService.GetOrderDetail(id);
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
            
        });
    }
    
    [Route("getAllProduct")]
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
    
    [Route("get All Clothing Product")]
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
    
    [Route("get All Electronique  Product")]
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
    
    [Route("get All food Product")]
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
    
    
    
    [Route("get all location")]
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
    
    [Route("get free location")]
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
    
    [Route("get all stock movement")]
    [HttpGet]
    public async Task<IActionResult> Index10()
    {
        var stockMovements = await _stockMovementService.GetAllStockMovements();
        return Json(stockMovements ,new JsonSerializerOptions{
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
    
[Route("transferproductblock")]
[HttpPost]
public async Task<IActionResult> Index12([FromBody] TransferRequest request)
{ 
    try{
    var transferresult = await _madeStockMovement.TransferProductBlockAsync(request.ProductBlockId, request.NewLocationId);
    return Json(transferresult, new JsonSerializerOptions
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        WriteIndented = false,
    });
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}

public class TransferRequest
{
    public int ProductBlockId { get; set; }
    public int NewLocationId { get; set; }
}
    
    [Route("mergeproductblocks")]
    [HttpPost]
    public async Task<IActionResult> Index13([FromBody] MergeRequest request)
    {
        try
        {
            
        var mergeresult = await _madeStockMovement.MergeProductBlocksAsync(request.SourceBlockId, request.DestinationBlockId);
        return Json(mergeresult, new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
        });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    public class MergeRequest
{
    public int SourceBlockId { get; set; }
    public int DestinationBlockId { get; set; }
}
    
    [Route("get ItemFrom A specific Buy order and a product")]
    [HttpGet]
    public async Task<IActionResult> Index14(int OrderId , int ProductId)
    {
        var productWithItems = await _stockMovementService.GetItemsForEachProductForSpecificBuyOrder(OrderId, ProductId);
        return Json(productWithItems ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("get Item From A specific order for all product")]
    [HttpGet]
    
    public async Task<IActionResult> Index15(int OrderId)
    {
        var productsWithItems = await _orderProductsRepository.GetProductsByOrderIdAsync(OrderId);
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
    
    [Route("get ItemFrom A specific Sell order and a product")]
    [HttpGet]
    public async Task<IActionResult> Index16(int OrderId , int ProductId)
    {
        var productWithItems = await _stockMovementService.GetItemsForEachProductForSpecificSellOrder(OrderId, ProductId);
        return Json(productWithItems ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("get Item From A specific Sell order for all product")]
    [HttpGet]
    
    public async Task<IActionResult> Index17(int OrderId)
    {
        var productlist = await _orderProductsRepository.GetProductsByOrderIdAsync(OrderId);
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
    
    [Route("deleteproductblock")]
    [HttpPost]
    public async Task<IActionResult> Index18(deleteRequest request)
    {
        try
        {
        var deleteresult = await _madeStockMovement.DeleteProductBlockAsync(request.ProductBlockId);
        return Json(deleteresult ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    public class deleteRequest
    {
        public int ProductBlockId { get; set; }
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
    
    [HttpGet]
    [Route("getPredection")]
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