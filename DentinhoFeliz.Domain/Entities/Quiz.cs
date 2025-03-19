namespace DentinhoFeliz.Domain.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Pergunta { get; set; } = string.Empty;
        public string[] Opcoes { get; set; } = Array.Empty<string>();
        public string Resposta { get; set; } = string.Empty;
    }
}