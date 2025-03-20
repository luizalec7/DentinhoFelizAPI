using System;

namespace DentinhoFeliz.Application.DTOs
{
    public class AlarmeDTO
    {
        public int Id { get; set; }
        public DateTime Horario { get; set; } = DateTime.UtcNow; // Define um hor�rio padr�o
        public string Mensagem { get; set; } = string.Empty; // Previne valores nulos
    }
}