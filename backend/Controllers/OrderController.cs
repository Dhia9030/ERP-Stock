using Microsoft.AspNetCore.Mvc;
using StockManagement.Services;

namespace StockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpPost("executeBuyOrder/{orderId}")]
        public async Task<IActionResult> ExecuteBuyOrderAsync(int orderId)
        {
            try
            {
                await _orderService.ExecuteBuyOrderAsync(orderId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("executeSellOrder/{orderId}")]
        public async Task<IActionResult> ExecuteSellOrderAsync(int orderId)
        {
            try
            {
                await _orderService.ExecuteSellOrderAsync(orderId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    
}