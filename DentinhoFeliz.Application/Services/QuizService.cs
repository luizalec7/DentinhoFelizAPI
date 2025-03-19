using DentinhoFeliz.Application.DTOs;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;

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
            return _context.Quizzes.Select(q => new QuizDTO
            {
                Id = q.Id,
                Pergunta = q.Pergunta,
                Opcoes = q.Opcoes
            }).ToList();
        }
    }
}