using DentinhoFeliz.Application.DTOs;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DentinhoFeliz.Application.Services
{
    public class AlarmeService
    {
        private readonly DentinhoFelizContext _context;

        public AlarmeService(DentinhoFelizContext context)
        {
            _context = context;
        }

        // Listar todos os alarmes
        public List<AlarmeDTO> GetAlarmes()
        {
            return _context.Alarmes
                .Select(a => new AlarmeDTO
                {
                    Id = a.Id,
                    Horario = a.Horario,
                    Mensagem = a.Mensagem
                })
                .ToList();
        }

        // Buscar um alarme por ID
        public AlarmeDTO? GetAlarmeById(int id)
        {
            var alarme = _context.Alarmes.Find(id);
            if (alarme == null) return null;

            return new AlarmeDTO
            {
                Id = alarme.Id,
                Horario = alarme.Horario,
                Mensagem = alarme.Mensagem
            };
        }

        // Criar um novo alarme
        public bool CriarAlarme(AlarmeDTO alarmeDto)
        {
            if (alarmeDto == null) return false;

            var alarme = new Alarme
            {
                Horario = alarmeDto.Horario,
                Mensagem = alarmeDto.Mensagem
            };

            try
            {
                _context.Alarmes.Add(alarme);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Atualizar um alarme existente
        public bool AtualizarAlarme(int id, AlarmeDTO alarmeDto)
        {
            var alarmeExistente = _context.Alarmes.Find(id);
            if (alarmeExistente == null) return false;

            alarmeExistente.Horario = alarmeDto.Horario;
            alarmeExistente.Mensagem = alarmeDto.Mensagem;

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Deletar um alarme
        public bool DeletarAlarme(int id)
        {
            var alarme = _context.Alarmes.Find(id);
            if (alarme == null) return false;

            try
            {
                _context.Alarmes.Remove(alarme);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
