using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;


[Route("api/[controller]")]
[ApiController]
public class StockMovementController : Controller
{
    private readonly IStockMovementService _stockMovementService;
    private readonly IMadeStockMovement _madeStockMovement;
    
    public StockMovementController(IStockMovementService stockMovementService, IMadeStockMovement madeStockMovement)
    {
        _stockMovementService = stockMovementService;
        _madeStockMovement = madeStockMovement;
    }

    
    
    
    
    [Route("GetAllStockMovement")]
    [HttpGet]
    public async Task<IActionResult> Index10()
    {
        var stockMovements = await _stockMovementService.GetAllStockMovements();
        return Json(stockMovements ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    
    
       
    [Route("TransferProductBlock")]
    [HttpPost]
    public async Task<IActionResult> Index12([FromBody] TransferRequest0 request)
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

    public class TransferRequest0
    {
        public int ProductBlockId { get; set; }
        public int NewLocationId { get; set; }
    }
    
    [Route("MergeProductBlocks")]
    [HttpPost]
    public async Task<IActionResult> Index13([FromBody] MergeRequest0 request)
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
    public class MergeRequest0
    {
        public int SourceBlockId { get; set; }
        public int DestinationBlockId { get; set; }
    }
    
    
    
    
    [Route("DeleteProductBlock")]
    [HttpPost]
    public async Task<IActionResult> Index18(deleteRequest0 request)
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
    
    public class deleteRequest0
    {
        public int ProductBlockId { get; set; }
    }
    
    
    
       
    
}
