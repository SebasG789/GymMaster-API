using GymSystemApi.DTOs;
using GymSystemApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemApi.Controllers
{
    [ApiController]
    [Route("api/rutinas")]
    [Authorize] // Solo usuarios autenticados pueden acceder
    public class RutinaController : ControllerBase
    {
        private readonly RutinaService _rutinaService;

        // Inyectamos el servicio de rutinas
        public RutinaController(RutinaService rutinaService)
        {
            _rutinaService = rutinaService;
        }

        // GET api/rutinas — Solo Admin y Entrenador pueden ver todas
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var rutinas = await _rutinaService.GetAllAsync();
            return Ok(rutinas);
        }

        // GET api/rutinas/{id} — Cualquier usuario autenticado
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rutina = await _rutinaService.GetByIdAsync(id);
            if (rutina == null)
                return NotFound(new { mensaje = "Rutina no encontrada" });

            return Ok(rutina);
        }

        // GET api/rutinas/cliente/{clienteId} — Cliente ve sus rutinas
        [HttpGet("cliente/{clienteId}")]
        [Authorize]
        public async Task<IActionResult> GetByCliente(int clienteId)
        {
            var rutinas = await _rutinaService.GetByClienteAsync(clienteId);
            return Ok(rutinas);
        }

        // POST api/rutinas — Solo Admin y Entrenador pueden crear
        [HttpPost]
        [Authorize(Roles = "Admin,Entrenador")]
        public async Task<IActionResult> Create(RutinaDto dto)
        {
            var rutina = await _rutinaService.CreateAsync(dto);
            if (rutina == null)
                return BadRequest(new { mensaje = "El cliente no existe" });

            return CreatedAtAction(nameof(GetById), new { id = rutina.Id }, rutina);
        }

        // PUT api/rutinas/{id} — Solo Admin y Entrenador pueden editar
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Entrenador")]
        public async Task<IActionResult> Update(int id, RutinaDto dto)
        {
            var resultado = await _rutinaService.UpdateAsync(id, dto);
            if (!resultado)
                return NotFound(new { mensaje = "Rutina no encontrada" });

            return Ok(new { mensaje = "Rutina actualizada correctamente" });
        }

        // DELETE api/rutinas/{id} — Solo Admin puede eliminar
        [HttpDelete("{id}")]
        [Authorize(Roles = "Entrenador")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _rutinaService.DeleteAsync(id);
            if (!resultado)
                return NotFound(new { mensaje = "Rutina no encontrada" });

            return Ok(new { mensaje = "Rutina eliminada correctamente" });
        }
    }
}
