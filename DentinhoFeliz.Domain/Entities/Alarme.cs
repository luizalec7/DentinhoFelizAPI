namespace DentinhoFeliz.Domain.Entities
{
    public class Alarme
    {
        public int Id { get; set; }
        public DateTime Horario { get; set; } = DateTime.UtcNow;
        public string Mensagem { get; set; } = string.Empty;
    }
}