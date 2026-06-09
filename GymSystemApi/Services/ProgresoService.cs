using GymSystemApi.Data;
using GymSystemApi.DTOs;
using GymSystemApi.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace GymSystemApi.Services
{
    public class ProgresoService
    {
        private readonly AppDbContext _context;

        // Inyectamos el contexto de la base de datos
        public ProgresoService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos los registros de progreso de un cliente
        public async Task<List<ProgresoResponseDto>> GetByClienteAsync(int clienteId)
        {
            return await _context.RegistrosProgreso
                .Where(rp => rp.ClienteId == clienteId)
                .OrderByDescending(rp => rp.Fecha)
                .Select(rp => new ProgresoResponseDto
                {
                    Id = rp.Id,
                    Fecha = rp.Fecha,
                    Peso = rp.Peso,
                    Notas = rp.Notas,
                    ClienteId = rp.ClienteId
                }).ToListAsync();
        }

        // Retorna un registro por id
        public async Task<ProgresoResponseDto?> GetByIdAsync(int id)
        {
            return await _context.RegistrosProgreso
                .Where(rp => rp.Id == id)
                .Select(rp => new ProgresoResponseDto
                {
                    Id = rp.Id,
                    Fecha = rp.Fecha,
                    Peso = rp.Peso,
                    Notas = rp.Notas,
                    ClienteId = rp.ClienteId
                }).FirstOrDefaultAsync();
        }

        // Crea un nuevo registro de progreso
        public async Task<ProgresoResponseDto?> CreateAsync(ProgresoDto dto)
        {
            // Verifica que el cliente exista
            var cliente = await _context.Usuarios.FindAsync(dto.ClienteId);
            if (cliente == null) return null;

            var registro = new RegistroProgreso
            {
                Peso = dto.Peso,
                Notas = dto.Notas,
                ClienteId = dto.ClienteId,
                Fecha = DateTime.UtcNow
            };

            _context.RegistrosProgreso.Add(registro);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(registro.Id);
        }

        // Actualiza un registro de progreso
        public async Task<bool> UpdateAsync(int id, ProgresoDto dto)
        {
            var registro = await _context.RegistrosProgreso.FindAsync(id);
            if (registro == null) return false;

            // Actualiza los campos
            registro.Peso = dto.Peso;
            registro.Notas = dto.Notas;

            await _context.SaveChangesAsync();
            return true;
        }

        // Elimina un registro de progreso
        public async Task<bool> DeleteAsync(int id)
        {
            var registro = await _context.RegistrosProgreso.FindAsync(id);
            if (registro == null) return false;

            _context.RegistrosProgreso.Remove(registro);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
