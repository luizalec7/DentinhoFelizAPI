using System.ComponentModel.DataAnnotations;

namespace DentinhoFeliz.Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail deve ter um formato válido.")]
        [StringLength(150, ErrorMessage = "O e-mail pode ter no máximo 150 caracteres.")]
        public string Email { get; set; } = string.Empty;
    }
}