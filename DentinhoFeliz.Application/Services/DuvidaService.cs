using DentinhoFeliz.Application.DTOs;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;

namespace DentinhoFeliz.Application.Services
{
    public class DuvidaService
    {
        private readonly DentinhoFelizContext _context;

        public DuvidaService(DentinhoFelizContext context)
        {
            _context = context;
        }

        public List<DuvidaDTO> GetDuvidas()
        {
            return _context.Duvidas.Select(d => new DuvidaDTO
            {
                Id = d.Id,
                Pergunta = d.Pergunta
            }).ToList();
        }
    }
}