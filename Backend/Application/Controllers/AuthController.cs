using Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Dto;
using Repository.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;
        public AuthController(
             UserManager<User> _userManager
            ,SignInManager<User> _signInManager
            ,IConfiguration _configuration)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            configuration = _configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!registerDto.Password.Equals(registerDto.ConfirmPassword))
            {
                return BadRequest("Password not Correct");
            }
            var user = new User
            {
                FirstName = registerDto.FirstName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded) 
            {
                await userManager.AddToRoleAsync(user, RolesConstants.User);
                return Ok(new { success = true, message = "Registration successful" });
            }
            
            return BadRequest(new { success = false, errors = result.Errors });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginDto loginDto)
        {
            var result = await signInManager.PasswordSignInAsync(
                            loginDto.Email, loginDto.Password, loginDto.RememberMe,
                            lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);
                if (user == null) return Unauthorized(new { isAuthenticated = false });
                var role = await userManager.GetRolesAsync(user);
                return Ok(new 
                { 
                    success = true, 
                    message = "Login successful", 
                    isAuthenticated = true,
                    userName = user.FirstName,
                    role,
                    roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList(),
                });
            }

            return Unauthorized(new { success = false, errors = "ERROR"});
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Ok(new { success = true, message = "LogOut successful" });
        }

        [HttpGet("forbidden")]
        public IActionResult Forbidden()
        {
            return Forbid();
        }
    }
}
