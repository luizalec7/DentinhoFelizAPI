using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentinhoFeliz.Domain.Entities
{
    [Table("DUVIDA")] // Define a tabela no banco de dados Oracle
    public class Duvida
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "A pergunta é obrigatória.")]
        [Column("PERGUNTA", TypeName = "NVARCHAR2(2000)")]
        public string Pergunta { get; set; } = string.Empty;

        [Required(ErrorMessage = "A resposta é obrigatória.")]
        [Column("RESPOSTA", TypeName = "NVARCHAR2(2000)")]
        public string Resposta { get; set; } = string.Empty;
    }
}