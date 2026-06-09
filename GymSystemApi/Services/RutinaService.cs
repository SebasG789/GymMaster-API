using GymSystemApi.Data;
using GymSystemApi.DTOs;
using GymSystemApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymSystemApi.Services
{
    public class RutinaService
    {
        private readonly AppDbContext _context;

        // Inyectamos el contexto de la base de datos
        public RutinaService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todas las rutinas con sus ejercicios
        public async Task<List<RutinaResponseDto>> GetAllAsync()
        {
            return await _context.Rutinas
                .Include(r => r.Cliente)
                .Include(r => r.Ejercicios)
                .Select(r => new RutinaResponseDto
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    DiasSemana = r.DiasSemana,
                    FechaCreacion = r.FechaCreacion,
                    ClienteId = r.ClienteId,
                    NombreCliente = r.Cliente!.Nombre,
                    Ejercicios = r.Ejercicios.Select(e => new EjercicioResponseDto
                    {
                        Id = e.Id,
                        Nombre = e.Nombre,
                        Series = e.Series,
                        Repeticiones = e.Repeticiones,
                        PesoKg = e.PesoKg,
                        Dia = e.Dia
                    }).ToList()
                }).ToListAsync();
        }

        // Retorna una rutina por id
        public async Task<RutinaResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Rutinas
                .Include(r => r.Cliente)
                .Include(r => r.Ejercicios)
                .Where(r => r.Id == id)
                .Select(r => new RutinaResponseDto
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    DiasSemana = r.DiasSemana,
                    FechaCreacion = r.FechaCreacion,
                    ClienteId = r.ClienteId,
                    NombreCliente = r.Cliente!.Nombre,
                    Ejercicios = r.Ejercicios.Select(e => new EjercicioResponseDto
                    {
                        Id = e.Id,
                        Nombre = e.Nombre,
                        Series = e.Series,
                        Repeticiones = e.Repeticiones,
                        PesoKg = e.PesoKg,
                        Dia = e.Dia
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        // Crea una nueva rutina
        public async Task<RutinaResponseDto?> CreateAsync(RutinaDto dto)
        {
            // Verifica que el cliente exista
            var cliente = await _context.Usuarios.FindAsync(dto.ClienteId);
            if (cliente == null) return null;

            var rutina = new Rutina
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                DiasSemana = dto.DiasSemana,
                ClienteId = dto.ClienteId
            };

            _context.Rutinas.Add(rutina);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(rutina.Id);
        }

        // Actualiza una rutina existente
        public async Task<bool> UpdateAsync(int id, RutinaDto dto)
        {
            var rutina = await _context.Rutinas.FindAsync(id);
            if (rutina == null) return false;

            // Actualiza los campos
            rutina.Nombre = dto.Nombre;
            rutina.Descripcion = dto.Descripcion;
            rutina.DiasSemana = dto.DiasSemana;
            rutina.ClienteId = dto.ClienteId;

            await _context.SaveChangesAsync();
            return true;
        }

        // Elimina una rutina
        public async Task<bool> DeleteAsync(int id)
        {
            var rutina = await _context.Rutinas.FindAsync(id);
            if (rutina == null) return false;

            _context.Rutinas.Remove(rutina);
            await _context.SaveChangesAsync();
            return true;
        }

        // Retorna las rutinas de un cliente específico
        public async Task<List<RutinaResponseDto>> GetByClienteAsync(int clienteId)
        {
            return await _context.Rutinas
                .Include(r => r.Cliente)
                .Include(r => r.Ejercicios)
                .Where(r => r.ClienteId == clienteId)
                .Select(r => new RutinaResponseDto
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    DiasSemana = r.DiasSemana,
                    FechaCreacion = r.FechaCreacion,
                    ClienteId = r.ClienteId,
                    NombreCliente = r.Cliente!.Nombre,
                    Ejercicios = r.Ejercicios.Select(e => new EjercicioResponseDto
                    {
                        Id = e.Id,
                        Nombre = e.Nombre,
                        Series = e.Series,
                        Repeticiones = e.Repeticiones,
                        PesoKg = e.PesoKg,
                        Dia = e.Dia
                    }).ToList()
                }).ToListAsync();
        }
    }
}
