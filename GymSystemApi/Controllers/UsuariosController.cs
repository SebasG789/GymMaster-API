using GymSystemApi.DTOs;
using GymSystemApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemApi.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize] // Solo usuarios autenticados pueden acceder
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        // Inyectamos el servicio de usuarios
        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET api/usuarios — Solo Admin puede ver todos los usuarios
        [HttpGet]
        [Authorize(Roles = "Admin, Entrenador")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        // GET api/usuarios/{id} — Admin y Entrenador
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Entrenador")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            return Ok(usuario);
        }

        // PUT api/usuarios/{id} — Solo Admin puede editar usuarios
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UsuarioUpdateDto dto)
        {
            var resultado = await _usuarioService.UpdateAsync(id, dto);
            if (!resultado)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            return Ok(new { mensaje = "Usuario actualizado correctamente" });
        }

        // DELETE api/usuarios/{id} — Solo Admin puede eliminar usuarios
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _usuarioService.DeleteAsync(id);
            if (!resultado)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            return Ok(new { mensaje = "Usuario eliminado correctamente" });
        }
    }
}
