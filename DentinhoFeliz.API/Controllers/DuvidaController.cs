using Microsoft.AspNetCore.Mvc;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;

namespace DentinhoFeliz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DuvidaController : ControllerBase
    {
        private readonly DentinhoFelizContext _context;

        public DuvidaController(DentinhoFelizContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDuvidas()
        {
            return Ok(_context.Duvidas.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetDuvidaById(int id)
        {
            var duvida = _context.Duvidas.Find(id);
            if (duvida == null)
                return NotFound();
            return Ok(duvida);
        }

        [HttpPost]
        public IActionResult CriarDuvida([FromBody] Duvida duvida)
        {
            if (duvida == null)
                return BadRequest();

            _context.Duvidas.Add(duvida);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetDuvidaById), new { id = duvida.Id }, duvida);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarDuvida(int id, [FromBody] Duvida duvida)
        {
            var duvidaExistente = _context.Duvidas.Find(id);
            if (duvidaExistente == null)
                return NotFound();

            duvidaExistente.Pergunta = duvida.Pergunta;
            duvidaExistente.Resposta = duvida.Resposta;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarDuvida(int id)
        {
            var duvida = _context.Duvidas.Find(id);
            if (duvida == null)
                return NotFound();

            _context.Duvidas.Remove(duvida);
            _context.SaveChanges();
            return NoContent();
        }
    }
}