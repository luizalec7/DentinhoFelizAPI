using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentinhoFeliz.Domain.Entities
{
    [Table("USUARIO")] // Define a tabela no banco Oracle
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome � obrigat�rio.")]
        [Column("NOME", TypeName = "NVARCHAR2(2000)")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail � obrigat�rio.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inv�lido.")]
        [Column("EMAIL", TypeName = "NVARCHAR2(2000)")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha � obrigat�ria.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        [Column("SENHA", TypeName = "NVARCHAR2(2000)")]
        public string Senha { get; set; } = string.Empty;
    }
}