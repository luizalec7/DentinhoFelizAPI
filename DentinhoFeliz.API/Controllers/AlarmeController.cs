using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAlarmes()
        {
            var alarmes = await _context.Alarmes.ToListAsync();
            return Ok(alarmes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlarmeById(int id)
        {
            var alarme = await _context.Alarmes.FindAsync(id);
            if (alarme == null)
                return NotFound(new { message = "Alarme não encontrado." });

            return Ok(alarme);
        }

        [HttpPost]
        public async Task<IActionResult> CriarAlarme([FromBody] Alarme alarme)
        {
            if (alarme == null || !ModelState.IsValid)
                return BadRequest(new { message = "Dados inválidos." });

            await _context.Alarmes.AddAsync(alarme);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlarmeById), new { id = alarme.Id }, alarme);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAlarme(int id, [FromBody] Alarme alarme)
        {
            if (alarme == null || id != alarme.Id)
                return BadRequest(new { message = "IDs não correspondem ou dados inválidos." });

            var alarmeExistente = await _context.Alarmes.FindAsync(id);
            if (alarmeExistente == null)
                return NotFound(new { message = "Alarme não encontrado." });

            alarmeExistente.Horario = alarme.Horario;
            alarmeExistente.Mensagem = alarme.Mensagem;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Alarme atualizado com sucesso!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAlarme(int id)
        {
            var alarme = await _context.Alarmes.FindAsync(id);
            if (alarme == null)
                return NotFound(new { message = "Alarme não encontrado." });

            _context.Alarmes.Remove(alarme);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Alarme deletado com sucesso!" });
        }
    }
}