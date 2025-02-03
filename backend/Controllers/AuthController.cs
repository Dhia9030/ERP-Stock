using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Mvc;
using WebOrder.Models;

namespace backend.Controllers;

public class AuthController : Controller

{
    private readonly IAuthentificationService _authentificationService;
    
    public AuthController(IAuthentificationService authentificationService)
    {
        _authentificationService = authentificationService;
    }
    
    [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authentificationService.Login(loginCredentials);
            if (result == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(result);
        }
}