using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;

namespace DentinhoFeliz.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly DentinhoFelizContext _context;
		private readonly IConfiguration _configuration;

		public AuthController(DentinhoFelizContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		[HttpPost("register")]
		public IActionResult Register([FromBody] Usuario usuario)
		{
			if (_context.Usuarios.Any(u => u.Email == usuario.Email))
				return BadRequest("E-mail já cadastrado.");

			_context.Usuarios.Add(usuario);
			_context.SaveChanges();
			return Ok("Usuário registrado com sucesso!");
		}

		[HttpPost("login")]
		public IActionResult Login([FromBody] Usuario usuario)
		{
			var user = _context.Usuarios.FirstOrDefault(u => u.Email == usuario.Email && u.Senha == usuario.Senha);
			if (user == null)
				return Unauthorized("E-mail ou senha incorretos.");

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Email) }),
				Expires = DateTime.UtcNow.AddHours(2),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return Ok(new { token = tokenHandler.WriteToken(token) });
		}
	}
}
