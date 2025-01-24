using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebOrder.JWTBearerConfig;
using WebOrder.Models;


namespace WebOrder.Controllers
{
    public class AuthController : Controller
    {
        private readonly JWTBearerTokenSettings jwtBearerTokenSettings;
        private readonly UserManager<IdentityUser> userManager;
        public SignInManager<IdentityUser> signInManager { get; set; }

        public AuthController(IOptions<JWTBearerTokenSettings> jwtTokenOptions, UserManager<IdentityUser> userManager , SignInManager<IdentityUser> signInManager)
        {
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }        
        
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login( LoginCredentials credentials)
        {
            IdentityUser ApplicationUser;

            if (!ModelState.IsValid || credentials == null || (ApplicationUser = await ValidateUser(credentials)) == null)
            {
                ViewBag.errorMessage = "Login failed";
                return View();
            }

            var token = await GenerateToken(ApplicationUser);
            HttpContext.Session.SetString("BearerToken", token);
            
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            // Well, What do you want to do here ?
            // Wait for token to get expired OR
            // Maintain token cache and invalidate the tokens after logout method
            HttpContext.Session.Remove("BearerToken");
            return RedirectToAction("Index", "Home");
        }

        private async Task<IdentityUser> ValidateUser(LoginCredentials credentials)
        {
            var ApplicationUser = await userManager.FindByNameAsync(credentials.Username);
            if (ApplicationUser != null)
            {
                var result = userManager.PasswordHasher.VerifyHashedPassword(ApplicationUser, ApplicationUser.PasswordHash, credentials.Password);
                return result == PasswordVerificationResult.Failed ? null : ApplicationUser;
            }
            return null;
        }

        private async Task<String> GenerateToken(IdentityUser ApplicationUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);
            
            var roles = await userManager.GetRolesAsync(ApplicationUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, ApplicationUser.UserName),
                new Claim(ClaimTypes.Email, ApplicationUser.Email),
                new Claim(ClaimTypes.NameIdentifier, ApplicationUser.Id), // Add NameIdentifier to get the user for the ClaimsPrincipal
            };

            // Add each role as a separate claim
            //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
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
}