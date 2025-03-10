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
                var user = await userManager.FindByEmailAsync(loginDto.Email);
                var token = GenerateJwtToken(user,loginDto.RememberMe);
                return Ok(new { success = true, message = "Login successful", token });
            }

            return BadRequest(new { success = false, errors = "ERROR"});
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Ok(new { success = true, message = "LogOut successful" });
        }

        private AccessTokenDto GenerateJwtToken(User user, bool rememberMe = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

            var expires = rememberMe ? DateTime.UtcNow.AddDays(1) : DateTime.UtcNow.AddHours(1);
            var expiresIn = (int)(expires - DateTime.UtcNow).TotalSeconds;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = new AccessTokenDto
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = expiresIn
            };
            return result;
        }

    }
}
