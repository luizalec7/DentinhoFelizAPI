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
    public class QuizController : ControllerBase
    {
        private readonly DentinhoFelizContext _context;

        public QuizController(DentinhoFelizContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuizzes()
        {
            var quizzes = await _context.Quizzes.ToListAsync();
            return Ok(quizzes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
                return NotFound(new { message = "Quiz não encontrado." });

            return Ok(quiz);
        }

        [HttpPost]
        public async Task<IActionResult> CriarQuiz([FromBody] Quiz quiz)
        {
            if (quiz == null || !ModelState.IsValid)
                return BadRequest(new { message = "Dados inválidos." });

            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuizById), new { id = quiz.Id }, quiz);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarQuiz(int id, [FromBody] Quiz quiz)
        {
            if (quiz == null || id != quiz.Id)
                return BadRequest(new { message = "IDs não correspondem ou dados inválidos." });

            var quizExistente = await _context.Quizzes.FindAsync(id);
            if (quizExistente == null)
                return NotFound(new { message = "Quiz não encontrado." });

            quizExistente.Pergunta = quiz.Pergunta;
            quizExistente.Resposta = quiz.Resposta;
            quizExistente.Opcoes = quiz.Opcoes;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Quiz atualizado com sucesso!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
                return NotFound(new { message = "Quiz não encontrado." });

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Quiz deletado com sucesso!" });
        }
    }
}