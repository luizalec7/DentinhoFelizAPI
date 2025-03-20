using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;
using System.Threading.Tasks;

namespace DentinhoFeliz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DentinhoFelizContext _context;

        public AuthController(DentinhoFelizContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario usuario)
        {
            if (usuario == null || !ModelState.IsValid)
                return BadRequest(new { message = "Dados inválidos." });

            if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
                return BadRequest(new { message = "E-mail já cadastrado." });

            // Criptografa a senha antes de salvar no banco
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Usuário registrado com sucesso!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
                return BadRequest(new { message = "E-mail e senha são obrigatórios." });

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(usuario.Senha, user.Senha))
                return Unauthorized(new { message = "E-mail ou senha incorretos." });

            // Retorna apenas os dados do usuário autenticado
            return Ok(new
            {
                user.Id,
                user.Nome,
                user.Email,
                message = "Login bem-sucedido!"
            });
        }
    }
}