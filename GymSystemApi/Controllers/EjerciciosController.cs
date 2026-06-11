using GymSystemApi.DTOs;
using GymSystemApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemApi.Controllers
{
    [ApiController]
    [Route("api/ejercicios")]
    [Authorize] // Solo usuarios autenticados pueden acceder
    public class EjerciciosController : ControllerBase
    {
        private readonly EjercicioService _ejercicioService;

        // Inyectamos el servicio de ejercicios
        public EjerciciosController(EjercicioService ejercicioService)
        {
            _ejercicioService = ejercicioService;
        }

        // GET api/ejercicios — Admin y Entrenador
        [HttpGet]
        [Authorize(Roles = "Admin,Entrenador,Cliente")]
        public async Task<IActionResult> GetAll()
        {
            var ejercicios = await _ejercicioService.GetAllAsync();
            return Ok(ejercicios);
        }

        // GET api/ejercicios/{id} — Cualquier usuario autenticado
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ejercicio = await _ejercicioService.GetByIdAsync(id);
            if (ejercicio == null)
                return NotFound(new { mensaje = "Ejercicio no encontrado" });

            return Ok(ejercicio);
        }

        // POST api/ejercicios — Solo Admin y Entrenador
        [HttpPost]
        [Authorize(Roles = "Admin,Entrenador")]
        public async Task<IActionResult> Create(EjercicioDto dto)
        {
            var ejercicio = await _ejercicioService.CreateAsync(dto);
            if (ejercicio == null)
                return BadRequest(new { mensaje = "La rutina no existe" });

            return CreatedAtAction(nameof(GetById), new { id = ejercicio.Id }, ejercicio);
        }

        // PUT api/ejercicios/{id} — Solo Admin y Entrenador
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Entrenador")]
        public async Task<IActionResult> Update(int id, EjercicioDto dto)
        {
            var resultado = await _ejercicioService.UpdateAsync(id, dto);
            if (!resultado)
                return NotFound(new { mensaje = "Ejercicio no encontrado" });

            return Ok(new { mensaje = "Ejercicio actualizado correctamente" });
        }

        // DELETE api/ejercicios/{id} — Solo Entrenador
        [HttpDelete("{id}")]
        [Authorize(Roles = "Entrenador")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _ejercicioService.DeleteAsync(id);
            if (!resultado)
                return NotFound(new { mensaje = "Ejercicio no encontrado" });

            return Ok(new { mensaje = "Ejercicio eliminado correctamente" });
        }
    }
}
