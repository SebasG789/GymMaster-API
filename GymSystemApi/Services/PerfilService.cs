using GymSystemApi.Data;
using GymSystemApi.DTOs;
using GymSystemApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymSystemApi.Services
{
    public class PerfilService
    {
        private readonly AppDbContext _context;

        // Inyectamos el contexto de la base de datos
        public PerfilService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna el perfil de un usuario por su id
        public async Task<PerfilResponseDto?> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Perfiles
                .Include(p => p.Usuario)
                .Where(p => p.UsuarioId == usuarioId)
                .Select(p => new PerfilResponseDto
                {
                    Id = p.Id,
                    Edad = p.Edad,
                    Peso = p.Peso,
                    Altura = p.Altura,
                    Objetivo = p.Objetivo,
                    UsuarioId = p.UsuarioId,
                    NombreUsuario = p.Usuario!.Nombre
                }).FirstOrDefaultAsync();
        }

        // Crea un perfil nuevo
        public async Task<PerfilResponseDto?> CreateAsync(PerfilDtos dto)
        {
            // Verifica que el usuario exista
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null) return null;

            // Verifica que no tenga ya un perfil
            var perfilExistente = await _context.Perfiles
                .AnyAsync(p => p.UsuarioId == dto.UsuarioId);
            if (perfilExistente) return null;

            var perfil = new PerfilCliente
            {
                Edad = dto.Edad,
                Peso = dto.Peso,
                Altura = dto.Altura,
                Objetivo = dto.Objetivo,
                UsuarioId = dto.UsuarioId
            };

            _context.Perfiles.Add(perfil);
            await _context.SaveChangesAsync();

            return await GetByUsuarioIdAsync(dto.UsuarioId);
        }

        // Actualiza el perfil existente
        public async Task<bool> UpdateAsync(int usuarioId, PerfilDtos dto)
        {
            var perfil = await _context.Perfiles
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId);
            if (perfil == null) return false;

            // Actualiza los campos
            perfil.Edad = dto.Edad;
            perfil.Peso = dto.Peso;
            perfil.Altura = dto.Altura;
            perfil.Objetivo = dto.Objetivo;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
