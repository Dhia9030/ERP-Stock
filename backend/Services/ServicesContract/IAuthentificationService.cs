using Microsoft.AspNetCore.Mvc;
using WebOrder.Models;

namespace backend.Services.ServicesContract;

public interface IAuthentificationService
{
    public Task<Object> Login(LoginCredentials loginCredentials);
    public Task<IActionResult> Logout();
    //public Task<IActionResult> AddSaleManager(RegisterCredentials registerCredentials);
}