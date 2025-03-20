using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        public async Task<IActionResult> GetDuvidas()
        {
            var duvidas = await _context.Duvidas.ToListAsync();
            return Ok(duvidas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDuvidaById(int id)
        {
            var duvida = await _context.Duvidas.FindAsync(id);
            if (duvida == null)
                return NotFound(new { message = "D�vida n�o encontrada." });

            return Ok(duvida);
        }

        [HttpPost]
        public async Task<IActionResult> CriarDuvida([FromBody] Duvida duvida)
        {
            if (duvida == null || !ModelState.IsValid)
                return BadRequest(new { message = "Dados inv�lidos." });

            await _context.Duvidas.AddAsync(duvida);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDuvidaById), new { id = duvida.Id }, duvida);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarDuvida(int id, [FromBody] Duvida duvida)
        {
            if (duvida == null || id != duvida.Id)
                return BadRequest(new { message = "IDs n�o correspondem ou dados inv�lidos." });

            var duvidaExistente = await _context.Duvidas.FindAsync(id);
            if (duvidaExistente == null)
                return NotFound(new { message = "D�vida n�o encontrada." });

            duvidaExistente.Pergunta = duvida.Pergunta;
            duvidaExistente.Resposta = duvida.Resposta;

            await _context.SaveChangesAsync();
            return Ok(new { message = "D�vida atualizada com sucesso!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarDuvida(int id)
        {
            var duvida = await _context.Duvidas.FindAsync(id);
            if (duvida == null)
                return NotFound(new { message = "D�vida n�o encontrada." });

            _context.Duvidas.Remove(duvida);
            await _context.SaveChangesAsync();
            return Ok(new { message = "D�vida deletada com sucesso!" });
        }
    }
}