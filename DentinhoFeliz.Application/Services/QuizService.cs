using DentinhoFeliz.Application.DTOs;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;
using System.Linq;

namespace DentinhoFeliz.Application.Services
{
    public class QuizService
    {
        private readonly DentinhoFelizContext _context;

        public QuizService(DentinhoFelizContext context)
        {
            _context = context;
        }

        public List<QuizDTO> GetQuizzes()
        {
            return _context.Quizzes
                .AsEnumerable() // Converte a consulta para execução em memória
                .Select(q => new QuizDTO
                {
                    Id = q.Id,
                    Pergunta = q.Pergunta,
                    Opcoes = q.OpcoesString != null ? q.OpcoesString.Split(';') : Array.Empty<string>(), // Correção: remove o operador `?.`
                    Resposta = q.Resposta
                })
                .ToList();
        }

        public QuizDTO GetQuizById(int id)
        {
            var quiz = _context.Quizzes.Find(id);
            if (quiz == null) return null;

            return new QuizDTO
            {
                Id = quiz.Id,
                Pergunta = quiz.Pergunta,
                Opcoes = quiz.OpcoesString != null ? quiz.OpcoesString.Split(';') : Array.Empty<string>(), // Correção aplicada
                Resposta = quiz.Resposta
            };
        }

        public void CriarQuiz(QuizDTO quizDTO)
        {
            var quiz = new Quiz
            {
                Pergunta = quizDTO.Pergunta,
                OpcoesString = string.Join(";", quizDTO.Opcoes), // Correção: junta corretamente o array em string
                Resposta = quizDTO.Resposta
            };

            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
        }

        public bool AtualizarQuiz(int id, QuizDTO quizDTO)
        {
            var quiz = _context.Quizzes.Find(id);
            if (quiz == null) return false;

            quiz.Pergunta = quizDTO.Pergunta;
            quiz.OpcoesString = string.Join(";", quizDTO.Opcoes); // Correção aplicada
            quiz.Resposta = quizDTO.Resposta;

            _context.SaveChanges();
            return true;
        }

        public bool DeletarQuiz(int id)
        {
            var quiz = _context.Quizzes.Find(id);
            if (quiz == null) return false;

            _context.Quizzes.Remove(quiz);
            _context.SaveChanges();
            return true;
        }
    }
}