using AutoMapper;
using CarShopApi.Data;
using CarShopApi.Models.User;
using CarShopApi.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;
        private readonly IConfiguration configuration;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            logger.LogInformation($"Registration Attempt for {userDto}");
            try
            {
                var user = mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;
                var result = await userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await userManager.AddToRoleAsync(user, "User");
                return Accepted();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went wrong on {nameof(Register)}");
                return Problem($"Something went wrong on {nameof(Register)}", statusCode: 500);
            } 
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDto loginUserDto)
        {
            logger.LogInformation($"Login Attempt for {loginUserDto.Email}");
            try
            {
                var user = await userManager.FindByEmailAsync(loginUserDto.Email);
                var passwordValid = await userManager.CheckPasswordAsync(user, loginUserDto.Password);

                if (user == null || passwordValid == false)
                {
                    return Unauthorized(loginUserDto);
                }

                string tokenString = await GenerateTokenAsync(user);

                var response = new AuthResponse
                {
                    Email = loginUserDto.Email,
                    Token = tokenString,
                    UserId = user.Id,
                };

                return Accepted(response); //could be response; / Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went wrong on {nameof(Login)}");
                return Problem($"Something went wrong on {nameof(Login)}", statusCode: 500);
            }
        }

        private async Task<string> GenerateTokenAsync(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(ct => new Claim(ClaimTypes.Role, ct)).ToList();

            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["JwtSettings:Duration"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
