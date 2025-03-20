using DentinhoFeliz.Application.DTOs;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DentinhoFeliz.Application.Services
{
    public class DuvidaService
    {
        private readonly DentinhoFelizContext _context;

        public DuvidaService(DentinhoFelizContext context)
        {
            _context = context;
        }

        // Listar todas as dúvidas
        public List<DuvidaDTO> GetDuvidas()
        {
            return _context.Duvidas
                .Select(d => new DuvidaDTO
                {
                    Id = d.Id,
                    Pergunta = d.Pergunta,
                    Resposta = d.Resposta // Agora retornamos a resposta também!
                })
                .ToList();
        }

        // Buscar uma dúvida por ID
        public DuvidaDTO? GetDuvidaById(int id)
        {
            var duvida = _context.Duvidas.Find(id);
            if (duvida == null) return null;

            return new DuvidaDTO
            {
                Id = duvida.Id,
                Pergunta = duvida.Pergunta,
                Resposta = duvida.Resposta
            };
        }

        // Criar uma nova dúvida
        public bool CriarDuvida(DuvidaDTO duvidaDto)
        {
            if (duvidaDto == null) return false;

            var duvida = new Duvida
            {
                Pergunta = duvidaDto.Pergunta,
                Resposta = duvidaDto.Resposta
            };

            try
            {
                _context.Duvidas.Add(duvida);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Atualizar uma dúvida existente
        public bool AtualizarDuvida(int id, DuvidaDTO duvidaDto)
        {
            var duvidaExistente = _context.Duvidas.Find(id);
            if (duvidaExistente == null) return false;

            duvidaExistente.Pergunta = duvidaDto.Pergunta;
            duvidaExistente.Resposta = duvidaDto.Resposta;

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

        // Deletar uma dúvida
        public bool DeletarDuvida(int id)
        {
            var duvida = _context.Duvidas.Find(id);
            if (duvida == null) return false;

            try
            {
                _context.Duvidas.Remove(duvida);
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