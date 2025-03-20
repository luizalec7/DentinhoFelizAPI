using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;

namespace DentinhoFeliz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly DentinhoFelizContext _context;

        public UsuarioController(DentinhoFelizContext context)
        {
            _context = context;
        }

        // 🔹 [GET] Listar todos os usuários
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usuarios = _context.Usuarios.ToList();
            return Ok(usuarios);
        }

        // 🔹 [GET] Buscar usuário por ID
        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(usuario);
        }

        // 🔹 [POST] Criar um novo usuário
        [HttpPost]
        public IActionResult CriarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || !ModelState.IsValid)
                return BadRequest(new { message = "Dados inválidos." });

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario);
        }

        // 🔹 [PUT] Atualizar usuário por ID
        [HttpPut("{id}")]
        public IActionResult AtualizarUsuario(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null || id != usuario.Id)
                return BadRequest(new { message = "IDs não correspondem ou dados inválidos." });

            var usuarioExistente = _context.Usuarios.Find(id);
            if (usuarioExistente == null)
                return NotFound(new { message = "Usuário não encontrado." });

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Senha = usuario.Senha; // ⚠ Caso precise de criptografia, implementar aqui!

            _context.SaveChanges();
            return Ok(new { message = "Usuário atualizado com sucesso!" });
        }

        // 🔹 [DELETE] Remover usuário por ID
        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return Ok(new { message = "Usuário deletado com sucesso!" });
        }
    }
}