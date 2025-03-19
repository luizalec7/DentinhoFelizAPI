using Microsoft.AspNetCore.Mvc;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;

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
        public IActionResult GetQuizzes()
        {
            return Ok(_context.Quizzes.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetQuizById(int id)
        {
            var quiz = _context.Quizzes.Find(id);
            if (quiz == null)
                return NotFound();
            return Ok(quiz);
        }

        [HttpPost]
        public IActionResult CriarQuiz([FromBody] Quiz quiz)
        {
            if (quiz == null)
                return BadRequest();

            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetQuizById), new { id = quiz.Id }, quiz);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarQuiz(int id, [FromBody] Quiz quiz)
        {
            var quizExistente = _context.Quizzes.Find(id);
            if (quizExistente == null)
                return NotFound();

            quizExistente.Pergunta = quiz.Pergunta;
            quizExistente.Resposta = quiz.Resposta;
            quizExistente.Opcoes = quiz.Opcoes;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarQuiz(int id)
        {
            var quiz = _context.Quizzes.Find(id);
            if (quiz == null)
                return NotFound();

            _context.Quizzes.Remove(quiz);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
