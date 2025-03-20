using System.ComponentModel.DataAnnotations;

namespace DentinhoFeliz.Application.DTOs
{
    public class DuvidaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A pergunta � obrigat�ria.")]
        [StringLength(500, ErrorMessage = "A pergunta pode ter no m�ximo 500 caracteres.")]
        public string Pergunta { get; set; } = string.Empty;

        [Required(ErrorMessage = "A resposta � obrigat�ria.")]
        [StringLength(1000, ErrorMessage = "A resposta pode ter no m�ximo 1000 caracteres.")]
        public string Resposta { get; set; } = string.Empty;
    }
}