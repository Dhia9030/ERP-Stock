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
    public async Task<IActionResult> Login(LoginCredentials loginCredentials)
    {
        var result = await _authentificationService.Login(loginCredentials);
        return Ok(result);
    }
}