using GymSystemApi.Data;
using GymSystemApi.DTOs;
using GymSystemApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymSystemApi.Services
{
    public class EjercicioService
    {
        private readonly AppDbContext _context;

        // Inyectamos el contexto de la base de datos
        public EjercicioService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos los ejercicios
        public async Task<List<EjercicioResponseDto>> GetAllAsync()
        {
            return await _context.Ejercicios
                .Select(e => new EjercicioResponseDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Series = e.Series,
                    Repeticiones = e.Repeticiones,
                    PesoKg = e.PesoKg,
                    Dia = e.Dia
                }).ToListAsync();
        }

        // Retorna un ejercicio por id
        public async Task<EjercicioResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Ejercicios
                .Where(e => e.Id == id)
                .Select(e => new EjercicioResponseDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Series = e.Series,
                    Repeticiones = e.Repeticiones,
                    PesoKg = e.PesoKg,
                    Dia = e.Dia
                }).FirstOrDefaultAsync();
        }

        // Crea un nuevo ejercicio
        public async Task<EjercicioResponseDto?> CreateAsync(EjercicioDto dto)
        {
            // Verifica que la rutina exista
            var rutina = await _context.Rutinas.FindAsync(dto.RutinaId);
            if (rutina == null) return null;

            var ejercicio = new Ejercicio
            {
                Nombre = dto.Nombre,
                Series = dto.Series,
                Repeticiones = dto.Repeticiones,
                PesoKg = dto.PesoKg,
                Dia = dto.Dia,
                RutinaId = dto.RutinaId
            };

            _context.Ejercicios.Add(ejercicio);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(ejercicio.Id);
        }

        // Actualiza un ejercicio existente
        public async Task<bool> UpdateAsync(int id, EjercicioDto dto)
        {
            var ejercicio = await _context.Ejercicios.FindAsync(id);
            if (ejercicio == null) return false;

            // Actualiza los campos
            ejercicio.Nombre = dto.Nombre;
            ejercicio.Series = dto.Series;
            ejercicio.Repeticiones = dto.Repeticiones;
            ejercicio.PesoKg = dto.PesoKg;
            ejercicio.Dia = dto.Dia;
            ejercicio.RutinaId = dto.RutinaId;

            await _context.SaveChangesAsync();
            return true;
        }

        // Elimina un ejercicio
        public async Task<bool> DeleteAsync(int id)
        {
            var ejercicio = await _context.Ejercicios.FindAsync(id);
            if (ejercicio == null) return false;

            _context.Ejercicios.Remove(ejercicio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
