using System.ComponentModel.DataAnnotations;

namespace DentinhoFeliz.Application.DTOs
{
    public class QuizDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A pergunta é obrigatória.")]
        [StringLength(500, ErrorMessage = "A pergunta pode ter no máximo 500 caracteres.")]
        public string Pergunta { get; set; } = string.Empty;

        [Required(ErrorMessage = "As opções de resposta são obrigatórias.")]
        [MinLength(2, ErrorMessage = "O quiz deve ter pelo menos duas opções de resposta.")]
        public string[] Opcoes { get; set; } = Array.Empty<string>();

        [Required(ErrorMessage = "A resposta correta é obrigatória.")]
        [StringLength(200, ErrorMessage = "A resposta pode ter no máximo 200 caracteres.")]
        public string Resposta { get; set; } = string.Empty;
    }
}