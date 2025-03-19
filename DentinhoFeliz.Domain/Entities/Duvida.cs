namespace DentinhoFeliz.Domain.Entities
{
    public class Duvida
    {
        public int Id { get; set; }
        public string Pergunta { get; set; } = string.Empty;
        public string Resposta { get; set; } = string.Empty;
    }
}