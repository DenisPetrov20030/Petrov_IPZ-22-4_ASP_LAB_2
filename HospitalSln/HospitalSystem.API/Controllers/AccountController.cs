using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HospitalSystem.API.DTOs;

namespace HospitalSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IConfiguration _configuration;

		public AccountController(
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager,
			IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = new IdentityUser
			{
				UserName = model.UserName,
				Email = model.Email
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
			}

			await _userManager.AddToRoleAsync(user, model.Role);

			return Ok(new { message = "Користувача успішно зареєстровано" });
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = await _userManager.FindByNameAsync(model.UserName) 
				?? await _userManager.FindByEmailAsync(model.UserName);

			if (user == null)
			{
				return Unauthorized(new { message = "Невірне ім'я користувача або пароль" });
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

			if (!result.Succeeded)
			{
				return Unauthorized(new { message = "Невірне ім'я користувача або пароль" });
			}

			var roles = await _userManager.GetRolesAsync(user);
			var token = GenerateJwtToken(user, roles);

			return Ok(new AuthResponseDto
			{
				Token = token,
				UserName = user.UserName ?? string.Empty,
				Email = user.Email ?? string.Empty,
				Role = roles.FirstOrDefault() ?? string.Empty
			});
		}

		[Authorize]
		[HttpGet("profile")]
		public async Task<IActionResult> GetProfile()
		{
			var userName = User.Identity?.Name;
			if (string.IsNullOrEmpty(userName))
			{
				return Unauthorized();
			}

			var user = await _userManager.FindByNameAsync(userName);
			if (user == null)
			{
				return NotFound(new { message = "Користувача не знайдено" });
			}

			var roles = await _userManager.GetRolesAsync(user);

			return Ok(new
			{
				userName = user.UserName,
				email = user.Email,
				role = roles.FirstOrDefault()
			});
		}

		private string GenerateJwtToken(IdentityUser user, IList<string> roles)
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var secretKey = jwtSettings["SecretKey"] ?? "YourSecretKeyForJWTTokenGeneration123456789";
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
				new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var token = new JwtSecurityToken(
				issuer: jwtSettings["Issuer"],
				audience: jwtSettings["Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiryMinutes"] ?? "60")),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
