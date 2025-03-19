using DentinhoFeliz.Application.DTOs;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;

namespace DentinhoFeliz.Application.Services
{
    public class UsuarioService
    {
        private readonly DentinhoFelizContext _context;

        public UsuarioService(DentinhoFelizContext context)
        {
            _context = context;
        }

        public List<UsuarioDTO> GetUsuarios()
        {
            return _context.Usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            }).ToList();
        }

        public UsuarioDTO GetUsuarioById(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }
    }
}