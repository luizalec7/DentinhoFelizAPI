using System.ComponentModel.DataAnnotations;

namespace DentinhoFeliz.Application.DTOs
{
    public class QuizDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A pergunta � obrigat�ria.")]
        [StringLength(500, ErrorMessage = "A pergunta pode ter no m�ximo 500 caracteres.")]
        public string Pergunta { get; set; } = string.Empty;

        [Required(ErrorMessage = "As op��es de resposta s�o obrigat�rias.")]
        [MinLength(2, ErrorMessage = "O quiz deve ter pelo menos duas op��es de resposta.")]
        public string[] Opcoes { get; set; } = Array.Empty<string>();

        [Required(ErrorMessage = "A resposta correta � obrigat�ria.")]
        [StringLength(200, ErrorMessage = "A resposta pode ter no m�ximo 200 caracteres.")]
        public string Resposta { get; set; } = string.Empty;
    }
}