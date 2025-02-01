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
    
    public TestController(AppDbContext db , IOrderRepository orderRepository, 
        IOrderProductsRepository orderProductsRepository , 
        IGetOrderService getOrderService, IGetProductService getProductService
        ,ILocationService locationService , IStockMovementService stockMovementService
        ,IProductWithBlocksService productWithBlocksService
        ,IMadeStockMovement madeStockMovement)
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
        var order = await _getProductService.GetAllProductsAsync();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("get All Clothing Product")]
    [HttpGet]
    public async Task<IActionResult> Index6()
    {
        var order = await _getProductService.GetAllClothingProduct();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
          
            
        });
    }
    
    [Route("get all location")]
    [HttpGet]
    public async Task<IActionResult> Index8()
    {
        var order = await _locationService.GetAllLocations();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
            
            
        });
    }
    
    [Route("get free location")]
    [HttpGet]
    public async Task<IActionResult> Index9()
    {
        var order = await _locationService.GetFreeLocations();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("get all stock movement")]
    [HttpGet]
    public async Task<IActionResult> Index10()
    {
        var order = await _stockMovementService.GetAllStockMovements();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("get all product with blocks")]
    [HttpGet]
    public async Task<IActionResult> Index11()
    {
        var order = await _productWithBlocksService.GetAllProductWithBlocks();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("transfer product block")]
    [HttpPost]
    public async Task<IActionResult> Index12(int productBlockId , int newLocationId)
    {
        var order = await _madeStockMovement.TransferProductBlockAsync(productBlockId, newLocationId);
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("merge product blocks")]
    [HttpPost]
    public async Task<IActionResult> Index13(int sourceBlockId , int destinationBlockId)
    {
        var order = await _madeStockMovement.MergeProductBlocksAsync(sourceBlockId, destinationBlockId);
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("get ItemFrom A specific Buy order and a product")]
    [HttpGet]
    public async Task<IActionResult> Index14(int OrderId , int ProductId)
    {
        var order = await _stockMovementService.GetItemsForEachProductForSpecificBuyOrder(OrderId, ProductId);
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("get Item From A specific order for all product")]
    [HttpGet]
    
    public async Task<IActionResult> Index15(int OrderId)
    {
        var productlist = await _orderProductsRepository.GetProductsByOrderIdAsync(OrderId);
        List<ProductWithItemsDto> productWithItemsDtos = new List<ProductWithItemsDto>();
        
        foreach (var product in productlist)
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
        var order = await _stockMovementService.GetItemsForEachProductForSpecificSellOrder(OrderId, ProductId);
        return Json(order ,new JsonSerializerOptions{
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
    
    [Route("delete product block")]
    [HttpPost]
    public async Task<IActionResult> Index18(int productBlockId)
    {
        var order = await _madeStockMovement.DeleteProductBlockAsync(productBlockId);
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    
    
    
    
    
    
    
    
    
}