using GymSystemApi.Data;
using GymSystemApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GymSystemApi.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        // Inyectamos el contexto de la base de datos
        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos los usuarios
        public async Task<List<UsuarioResponseDto>> GetAllAsync()
        {
            return await _context.Usuarios
                .Select(u => new UsuarioResponseDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    Rol = u.Rol,
                    FechaRegistro = u.FechaRegistro
                }).ToListAsync();
        }

        // Retorna un usuario por id
        public async Task<UsuarioResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new UsuarioResponseDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    Rol = u.Rol,
                    FechaRegistro = u.FechaRegistro
                }).FirstOrDefaultAsync();
        }

        // Actualiza un usuario existente
        public async Task<bool> UpdateAsync(int id, UsuarioUpdateDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            // Actualiza los campos
            usuario.Nombre = dto.Nombre;
            usuario.Email = dto.Email;
            usuario.Rol = dto.Rol;

            await _context.SaveChangesAsync();
            return true;
        }

        // Elimina un usuario
        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
