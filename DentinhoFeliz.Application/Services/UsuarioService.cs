using DentinhoFeliz.Application.DTOs;
using DentinhoFeliz.Domain.Entities;
using DentinhoFeliz.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DentinhoFeliz.Application.Services
{
    public class UsuarioService
    {
        private readonly DentinhoFelizContext _context;

        public UsuarioService(DentinhoFelizContext context)
        {
            _context = context;
        }

        // Listar todos os usu�rios
        public List<UsuarioDTO> GetUsuarios()
        {
            return _context.Usuarios
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email
                }).ToList();
        }

        // Buscar um usu�rio por ID
        public UsuarioDTO? GetUsuarioById(int id)
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

        // Criar um novo usu�rio
        public string CriarUsuario(Usuario usuario)
        {
            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
                return "E-mail j� cadastrado.";

            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return "Usu�rio criado com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao criar usu�rio: {ex.Message}";
            }
        }

        // Atualizar um usu�rio existente
        public string AtualizarUsuario(int id, Usuario usuarioAtualizado)
        {
            var usuarioExistente = _context.Usuarios.Find(id);
            if (usuarioExistente == null) return "Usu�rio n�o encontrado.";

            usuarioExistente.Nome = usuarioAtualizado.Nome;
            usuarioExistente.Email = usuarioAtualizado.Email;

            try
            {
                _context.SaveChanges();
                return "Usu�rio atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao atualizar usu�rio: {ex.Message}";
            }
        }

        // Deletar um usu�rio
        public string DeletarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return "Usu�rio n�o encontrado.";

            try
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
                return "Usu�rio deletado com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao deletar usu�rio: {ex.Message}";
            }
        }
    }
}