using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentinhoFeliz.Domain.Entities
{
    [Table("ALARME")] // Define o nome da tabela no banco de dados
    public class Alarme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O hor�rio � obrigat�rio.")]
        [Column("HORARIO", TypeName = "TIMESTAMP(7)")]
        public DateTime Horario { get; set; }

        [Required(ErrorMessage = "A mensagem � obrigat�ria.")]
        [Column("MENSAGEM", TypeName = "NVARCHAR2(2000)")]
        public string Mensagem { get; set; } = string.Empty;
    }
}