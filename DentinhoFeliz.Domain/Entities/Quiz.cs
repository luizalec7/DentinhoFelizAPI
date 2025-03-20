using System.ComponentModel.DataAnnotations.Schema;

namespace DentinhoFeliz.Domain.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Pergunta { get; set; } = string.Empty;

        [NotMapped] // Não mapear essa propriedade para o banco
        public string[] Opcoes
        {
            get => string.IsNullOrEmpty(OpcoesString) ? new string[0] : OpcoesString.Split(';');
            set => OpcoesString = string.Join(";", value);
        }

        public string OpcoesString { get; set; } = string.Empty; // Essa é a única que vai pro banco

        public string Resposta { get; set; } = string.Empty;
    }
}