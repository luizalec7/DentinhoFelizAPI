using Microsoft.AspNetCore.Mvc;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;

namespace DentinhoFeliz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlarmeController : ControllerBase
    {
        private readonly DentinhoFelizContext _context;

        public AlarmeController(DentinhoFelizContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAlarmes()
        {
            return Ok(_context.Alarmes.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetAlarmeById(int id)
        {
            var alarme = _context.Alarmes.Find(id);
            if (alarme == null)
                return NotFound();
            return Ok(alarme);
        }

        [HttpPost]
        public IActionResult CriarAlarme([FromBody] Alarme alarme)
        {
            if (alarme == null)
                return BadRequest();

            _context.Alarmes.Add(alarme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAlarmeById), new { id = alarme.Id }, alarme);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarAlarme(int id, [FromBody] Alarme alarme)
        {
            var alarmeExistente = _context.Alarmes.Find(id);
            if (alarmeExistente == null)
                return NotFound();

            alarmeExistente.Horario = alarme.Horario;
            alarmeExistente.Mensagem = alarme.Mensagem;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarAlarme(int id)
        {
            var alarme = _context.Alarmes.Find(id);
            if (alarme == null)
                return NotFound();

            _context.Alarmes.Remove(alarme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
