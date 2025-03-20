using System.ComponentModel.DataAnnotations;

namespace DentinhoFeliz.Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome � obrigat�rio.")]
        [StringLength(100, ErrorMessage = "O nome pode ter no m�ximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail � obrigat�rio.")]
        [EmailAddress(ErrorMessage = "O e-mail deve ter um formato v�lido.")]
        [StringLength(150, ErrorMessage = "O e-mail pode ter no m�ximo 150 caracteres.")]
        public string Email { get; set; } = string.Empty;
    }
}