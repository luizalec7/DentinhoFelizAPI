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

        // Listar todos os usuários
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

        // Buscar um usuário por ID
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

        // Criar um novo usuário
        public string CriarUsuario(Usuario usuario)
        {
            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
                return "E-mail já cadastrado.";

            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return "Usuário criado com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao criar usuário: {ex.Message}";
            }
        }

        // Atualizar um usuário existente
        public string AtualizarUsuario(int id, Usuario usuarioAtualizado)
        {
            var usuarioExistente = _context.Usuarios.Find(id);
            if (usuarioExistente == null) return "Usuário não encontrado.";

            usuarioExistente.Nome = usuarioAtualizado.Nome;
            usuarioExistente.Email = usuarioAtualizado.Email;

            try
            {
                _context.SaveChanges();
                return "Usuário atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao atualizar usuário: {ex.Message}";
            }
        }

        // Deletar um usuário
        public string DeletarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return "Usuário não encontrado.";

            try
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
                return "Usuário deletado com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao deletar usuário: {ex.Message}";
            }
        }
    }
}