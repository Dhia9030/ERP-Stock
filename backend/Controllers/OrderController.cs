using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Services;

namespace StockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IConfirmOrderService _confirmOrderService;
        
        

        public OrderController(IOrderService orderService , IConfirmOrderService confirmOrderService)
        {
            _orderService = orderService;
            _confirmOrderService = confirmOrderService;
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
        [HttpPost("ConfirmBuy/{orderId}")]
        public async Task<IActionResult> ExecuteReturnOrderAsync(int orderId)
        {
            try
            {
                await _confirmOrderService.ConfirmBuyOrderAsync(orderId);
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
        
        [HttpPost("ConfirmSale/{orderId}")]
        public async Task<IActionResult> ExecuteExpireOrderAsync(int orderId)
        {
            try
            {
                await _confirmOrderService.ConfirmSaleOrderAsync(orderId);
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