using DentinhoFeliz.Application.DTOs;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;

namespace DentinhoFeliz.Application.Services
{
    public class AlarmeService
    {
        private readonly DentinhoFelizContext _context;

        public AlarmeService(DentinhoFelizContext context)
        {
            _context = context;
        }

        public List<AlarmeDTO> GetAlarmes()
        {
            return _context.Alarmes.Select(a => new AlarmeDTO
            {
                Id = a.Id,
                Horario = a.Horario,
                Mensagem = a.Mensagem
            }).ToList();
        }
    }
}