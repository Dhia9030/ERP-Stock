using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.JWTBearerConfig;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebOrder.Models;

namespace backend.Services.ConcreteServices;

public class AuthentificationService : IAuthentificationService
{
    private readonly JWTBearerTokenSettings jwtBearerTokenSettings;
    private readonly UserManager<IdentityUser> userManager;

    
    public AuthentificationService( UserManager<IdentityUser> userManager, IOptions<JWTBearerTokenSettings> jwtBearerTokenSettings)
    {
        this.userManager = userManager;
        this.jwtBearerTokenSettings = jwtBearerTokenSettings.Value;
    }
   
    public async Task<Object> Login(LoginCredentials loginCredentials)
    {
        IdentityUser identityUser;

        if (loginCredentials == null || (identityUser = await ValidateUser(loginCredentials)) == null)
        {
            return new BadRequestObjectResult(new { Message = "Login failed" });
        }

        var token = await GenerateToken(identityUser);
        return (new { Token = token, Message = "Success" });
        
    }

    public async Task<IActionResult> Logout()
    {
        throw new System.NotImplementedException();
    }
    
    private async Task<IdentityUser> ValidateUser(LoginCredentials credentials)
    {
        var identityUser = await userManager.FindByNameAsync(credentials.Username);
        if (identityUser != null)
        {
            var result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
            return result == PasswordVerificationResult.Failed ? null : identityUser;
        }
        return null;
    }

    private async Task<object> GenerateToken(IdentityUser identityUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);
            
        var roles = await userManager.GetRolesAsync(identityUser);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, identityUser.UserName),
            new Claim(ClaimTypes.Email, identityUser.Email),
                
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpireTimeInSeconds),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = jwtBearerTokenSettings.Audience,
            Issuer = jwtBearerTokenSettings.Issuer
        };

            
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }


}