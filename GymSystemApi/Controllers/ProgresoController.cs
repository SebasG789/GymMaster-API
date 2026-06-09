using GymSystemApi.DTOs;
using GymSystemApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemApi.Controllers
{
    [ApiController]
    [Route("api/progreso")]
    [Authorize] // Solo usuarios autenticados pueden acceder
    public class ProgresoController : ControllerBase
    {
        private readonly ProgresoService _progresoService;

        // Inyectamos el servicio de progreso
        public ProgresoController(ProgresoService progresoService)
        {
            _progresoService = progresoService;
        }

        // GET api/progreso/cliente/{clienteId} — Admin, Entrenador o el mismo cliente
        [HttpGet("cliente/{clienteId}")]
        [Authorize]
        public async Task<IActionResult> GetByCliente(int clienteId)
        {
            var registros = await _progresoService.GetByClienteAsync(clienteId);
            return Ok(registros);
        }

        // GET api/progreso/{id} — Cualquier usuario autenticado
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var registro = await _progresoService.GetByIdAsync(id);
            if (registro == null)
                return NotFound(new { mensaje = "Registro no encontrado" });

            return Ok(registro);
        }

        // POST api/progreso — Cliente registra su propio progreso
        [HttpPost]
        public async Task<IActionResult> Create(ProgresoDto dto)
        {
            var registro = await _progresoService.CreateAsync(dto);
            if (registro == null)
                return BadRequest(new { mensaje = "El cliente no existe" });

            return CreatedAtAction(nameof(GetById), new { id = registro.Id }, registro);
        }

        // PUT api/progreso/{id} — Admin y Entrenador pueden editar
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Entrenador")]
        public async Task<IActionResult> Update(int id, ProgresoDto dto)
        {
            var resultado = await _progresoService.UpdateAsync(id, dto);
            if (!resultado)
                return NotFound(new { mensaje = "Registro no encontrado" });

            return Ok(new { mensaje = "Registro actualizado correctamente" });
        }

        // DELETE api/progreso/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _progresoService.DeleteAsync(id);
            if (!resultado)
                return NotFound(new { mensaje = "Registro no encontrado" });

            return Ok(new { mensaje = "Registro eliminado correctamente" });
        }
    }
}
